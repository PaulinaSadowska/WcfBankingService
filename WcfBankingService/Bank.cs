using System.Security.Authentication;
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
    public class Bank
    {
        private const string BankId = "11216900";

        private readonly UserManager _userManager;
        private readonly AccountNumberFactory _accountNumberFactory;
        private readonly PersistantExecutor _executor;
        private readonly IBankDataInserter _dataInserter;

        public Bank(IBankDataInserter dataInserter)
        {
            _accountNumberFactory = new AccountNumberFactory(BankId, new StandardControlSumCalculator());
            _userManager = new UserManager(new DbDataProvider(_accountNumberFactory));
            _executor = new PersistantExecutor(dataInserter);
            _dataInserter = dataInserter;
        }

        public LogInResponse SignIn(string login, string password)
        {
            try
            {
                var accessToken = _userManager.SignIn(login, password);
                _dataInserter.SaveAccessToken(login, accessToken);
                return new LogInResponse(accessToken);
            }
            catch (BankException exception)
            {
                return new LogInResponse(exception.ResponseStatus);
            }
        }

        public PaymentResponse Deposit(DepositData paymentData)
        {
            try
            {
                var account = GetAccount(paymentData.AccountNumber);
                _executor.ExecuteAndSave(new Deposit(account, paymentData.Amount, paymentData.OperationTitle), account);
                return new PaymentResponse(ResponseStatus.Success);
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
        }

        public PaymentResponse Withdraw(WithdrawData paymentData)
        {
            try
            {
                var account = GetAccount(paymentData.AccessToken, paymentData.AccountNumber);
                _executor.ExecuteAndSave(new Withdraw(account, paymentData.Amount, paymentData.OperationTitle), account);
                return new PaymentResponse(ResponseStatus.Success);
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
        }

        public PaymentResponse IncomingTransfer(TransferData transferData)
        {
            try
            {
                var account = GetAccount(transferData.AccountNumber);
                var amount = transferData.Amount/100m;
                _executor.ExecuteAndSave(new IncomingTransfer(account, amount, transferData.Title, transferData.SenderAccountNumber), account);
                return new PaymentResponse(ResponseStatus.Success);
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
        }

        public PaymentResponse SoapTransfer(TransferData transferData, string accessToken) //transfer from soap
        {
            AccountNumber receiverAccountNumber;
            IAccount sender;
            IPublicAccount receiver;
            try
            {
                 sender = GetAccount(accessToken, transferData.SenderAccountNumber);
                 receiverAccountNumber = _accountNumberFactory.GetAccountNumber(transferData.AccountNumber);
            }
            catch (BankException exception)
            {
                //not my account, access denied, or wrong receiver account number
                return new PaymentResponse(exception.ResponseStatus);
            }
            try
            {
                receiver = GetAccount(transferData.AccountNumber);
            }
            catch (BankException)
            {
                //receiver is not from this bank
                try
                {
                    var interTransfer = new InterBankTransfer(sender, receiverAccountNumber, transferData.Amount, transferData.Title);
                    _executor.ExecuteAndSave(interTransfer, sender);
                    return new PaymentResponse(ResponseStatus.Success);
                }
                catch (BankException e)
                {
                    return new PaymentResponse(e.ResponseStatus);
                }
            }
            //sender and receiver from my bank
            try
            {
                var innerTransfer = new InnerBankTransfer(sender, receiver, transferData.Amount, transferData.Title);
                _executor.ExecuteAndSave(innerTransfer, sender, receiver);
                return new PaymentResponse(ResponseStatus.Success);
            }
            catch (BankException e)
            {
                return new PaymentResponse(e.ResponseStatus);
            }

        }

        public OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber)
        {
            try
            {
                var account = GetAccount(accessToken, accountNumber);
                return new OperationHistoryResponse(account.GetOperationHistory());
            }
            catch (BankException exception)
            {
                return new OperationHistoryResponse(exception.ResponseStatus);
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

            var account = _userManager.GetAccount(accountNumber);
            if (account == null)
            {
                throw new BankException(ResponseStatus.AccountNumberDoesntExist);
            }
            return account;
        }
    }
}