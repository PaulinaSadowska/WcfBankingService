using System;
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
    public class Bank
    {
        private const string BankId = "00112169";

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
                throw new FaultException(exception.ResponseStatus.Message());
            }
        }

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

        public PaymentResponse SoapTransfer(SoapTransferData transferData) //transfer from soap
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

                var interTransfer = new InterBankTransfer(sender, receiverAccountNumber, amount,
                    transferData.Title);
                _executor.ExecuteAndSave(interTransfer, sender);
                return PrepareResponse(interTransfer.ResponseStatus);
            }
            //sender and receiver from my bank

            var innerTransfer = new InnerBankTransfer(sender, receiver, amount, transferData.Title);
            _executor.ExecuteAndSave(innerTransfer, sender, receiver);
            return PrepareResponse(innerTransfer.ResponseStatus);
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