using System.Linq;
using System.Security.Authentication;
using System.ServiceModel;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Accounts.Number.ControlSum;
using WcfBankingService.Database.DataProvider;
using WcfBankingService.Database.SavingData;
using WcfBankingService.operation.Complex;
using WcfBankingService.operation.operations;
using WcfBankingService.Operation.Complex;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Users;

namespace WcfBankingService
{
    /// <summary>
    /// Allows to pererform bank operations on user manager and save records of them
    /// </summary>
    public class Bank
    {
        private const string BankId = "00112169";

        private readonly UserManager _userManager;
        private readonly AccountNumberFactory _accountNumberFactory;
        private readonly PersistantExecutor _executor;
        private readonly IBankDataInserter _dataInserter;

        /// <summary>
        /// creates bank object
        /// </summary>
        /// <param name="dataInserter">data inserter user to save data into database</param>
        public Bank(IBankDataInserter dataInserter)
        {
            _accountNumberFactory = new AccountNumberFactory(BankId, new NRBControlSumCalculator());
            _userManager = new UserManager(new DbDataProvider(_accountNumberFactory));
            _executor = new PersistantExecutor(dataInserter);
            _dataInserter = dataInserter;
        }

        /// <summary>
        /// Tries to sign in with given login and password. When fails throws FaultException
        ///  </summary>
        /// <param name="login">user login</param>
        /// <param name="password">user password</param>
        /// <returns>response status and access token</returns>
        public LogInResponse SignIn(string login, string password)
        {
            try
            {
                var accessToken = _userManager.SignIn(login, password);
                var accounts = _userManager.GetAllAccountNumbers(login, accessToken).ToList();
                _dataInserter.SaveAccessToken(login, accessToken);
                return new LogInResponse(accessToken, accounts);
            }
            catch (BankException exception)
            {
                throw new FaultException(exception.ResponseStatus.Message());
            }
        }

        /// <summary>
        /// Tries to perform deposit operation, if fails throws Fault Exception
        /// </summary>
        /// <param name="paymentData">data needed to perform deposit</param>
        /// <returns>response status</returns>
        public PaymentResponse Deposit(DepositData paymentData)
        {
            try
            {
                var account = GetAccount(paymentData.AccountNumber);
                _executor.ExecuteAndSave(new Deposit(account, DecimalParser.Parse(paymentData.Amount), paymentData.OperationTitle), account);
                return new PaymentResponse(ResponseStatus.Success);
            }
            catch (BankException exception)
            {
                throw new FaultException(exception.ResponseStatus.Message());
            }
        }

        /// <summary>
        /// Tries to perform withdraw operation, if fails throws Fault Exception
        /// </summary>
        /// <param name="paymentData">data needed to perform withdraw</param>
        /// <returns>response status</returns>
        public PaymentResponse Withdraw(WithdrawData paymentData)
        {
            try
            {
                var account = GetAccount(paymentData.AccessToken, paymentData.AccountNumber);
                _executor.ExecuteAndSave(new Withdraw(account, DecimalParser.Parse(paymentData.Amount), paymentData.OperationTitle), account);
                return new PaymentResponse(ResponseStatus.Success);
            }
            catch (BankException exception)
            {
                throw new FaultException(exception.ResponseStatus.Message());
            }
        }

        /// <summary>
        /// Tries to perform transfer initialized by REST service. 
        /// If fails returnd PaymentResponse with error code and message.
        /// Blocks inner transfers because lack of permissions
        /// </summary>
        /// <param name="transferData">data needed to perform transfer</param>
        /// <returns>response status</returns>
        public PaymentResponse RestTransfer(TransferData transferData)
        {
            try
            {
                var account = GetAccount(transferData.AccountNumber);
                var amount = transferData.Amount/100m;
                try
                {
                    GetAccount(transferData.AccountNumber);
                    GetAccount(transferData.SenderAccountNumber);
                    //transfer fails when both accounts are from this bank
                    return new PaymentResponse(ResponseStatus.AccessDenied); 
                }
                catch (BankException exception)
                {
                    if (exception.ResponseStatus != ResponseStatus.AccountNumberDoesntExist &&
                        exception.ResponseStatus != ResponseStatus.OtherBankAccount)
                        return new PaymentResponse(exception.ResponseStatus);

                    _executor.ExecuteAndSave(
                        new IncomingTransfer(account, amount, transferData.Title, transferData.SenderAccountNumber),
                        account);
                    return new PaymentResponse(ResponseStatus.Success);
                }
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
        }

        /// <summary>
        /// Transfer called by Soap service. 
        /// Performs inner bank transfer when both account are from this bank or 
        /// inter bank transfer when receiver is from other (listed) bank.
        /// When transfer fails - throws FaultException
        /// </summary>
        /// <param name="transferData">data needed to perform transfer</param>
        /// <returns>response status</returns>
        public PaymentResponse SoapTransfer(SoapTransferData transferData) 
        {
            var amount = DecimalParser.Parse(transferData.Amount);
            AccountNumber receiverAccountNumber;
            IAccount sender;
            IPublicAccount receiver;
            try
            {
                sender = GetAccount(transferData.AccessToken, transferData.SenderAccountNumber);
                receiverAccountNumber = _accountNumberFactory.GetAccountNumber(transferData.ReceiverAccountNumber);
            }
            catch (BankException exception)
            {
                //not my account, access denied, or wrong receiver account number
                throw new FaultException(exception.ResponseStatus.Message());
            }
            try
            {
                receiver = GetAccount(transferData.ReceiverAccountNumber);
            }
            catch (BankException exception)
            {
                if (exception.ResponseStatus != ResponseStatus.OtherBankAccount)
                    throw new FaultException(exception.ResponseStatus.Message());

                //receiver ot from this bank - INTER BANK TRANSFER
                var interTransfer = new InterBankTransfer(sender, receiverAccountNumber, amount,
                    transferData.Title);
                _executor.ExecuteAndSave(interTransfer, sender);
                return PrepareResponse(interTransfer.ResponseStatus);
            }
            //sender and receiver from this bank - INNER TRANSFER

            var innerTransfer = new InnerBankTransfer(sender, receiver, amount, transferData.Title);
            _executor.ExecuteAndSave(innerTransfer, sender, receiver);
            return PrepareResponse(innerTransfer.ResponseStatus);
        }

        /// <summary>
        /// Fetches operation history for given account number. If permissions denied - throws FaultException
        /// </summary>
        /// <param name="accessToken">access token to verify permissions</param>
        /// <param name="accountNumber">account number which history should be fetched</param>
        /// <returns>operation records list with response status</returns>
        public OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber)
        {
            try
            {
                var account = GetAccount(accessToken, accountNumber);
                return new OperationHistoryResponse(account.GetOperationHistory());
            }
            catch (BankException exception)
            {
                throw new FaultException(exception.ResponseStatus.Message());
            }
        }


        private IAccount GetAccount(string accessToken, string accountNumberStr)
        {
            var accountNumber = _accountNumberFactory.GetAccountNumber(accountNumberStr);
            if (accountNumber == null)
                throw new BankException(ResponseStatus.WrongAccountNumber);
            try
            {
                var account = _userManager.GetAccount(accessToken, accountNumber);
                if (account == null)
                {
                    throw new BankException(ResponseStatus.AccountNumberDoesntExist);
                }
                return account;
            }
            catch (AuthenticationException)
            {
                throw new BankException(ResponseStatus.AccessDenied);
            }
        }

        private IPublicAccount GetAccount(string accountNumberStr)
        {
            var accountNumber = _accountNumberFactory.GetAccountNumber(accountNumberStr);
            if (accountNumber == null)
                throw new BankException(ResponseStatus.WrongAccountNumber);
            if (accountNumber.BankId != BankId)
                throw new BankException(ResponseStatus.OtherBankAccount);

            var account = _userManager.GetAccount(accountNumber);
            if (account == null)
            {
                throw new BankException(ResponseStatus.AccountNumberDoesntExist);
            }
            return account;
        }

        private static PaymentResponse PrepareResponse(ResponseStatus responseStatus)
        {
            if (responseStatus == ResponseStatus.Success)
                return new PaymentResponse(ResponseStatus.Success);
            throw new FaultException(responseStatus.Message());
        }
    }
}