using System;
using System.Runtime.Remoting.Channels;
using System.Security.Authentication;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Accounts.Number.ControlSum;
using WcfBankingService.Database.DataProvider;
using WcfBankingService.Database.SavingData;
using WcfBankingService.operation.operations;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.Users;

namespace WcfBankingService
{
    public class Bank
    {
        private const string BankId = "11216900";

        private readonly UserManager _userManager;
        private readonly AccountNumberFactory _accountNumberFactory;
        private readonly IBankDataInserter _dataInserter;

        public Bank(IBankDataInserter dataInserter)
        {
            _accountNumberFactory = new AccountNumberFactory(BankId, new StandardControlSumCalculator());
            _userManager = new UserManager(new DbDataProvider(_accountNumberFactory));
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
                ExecuteAndSave(account, new Deposit(account, paymentData.Amount, paymentData.OperationTitle));
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
            return new PaymentResponse(ResponseStatus.Success);
        }

        public PaymentResponse Withdraw(WithdrawData paymentData)
        {
            try
            {
                var account = GetAccount(paymentData.AccessToken, paymentData.AccountNumber);
                ExecuteAndSave(account, new Withdraw(account, paymentData.Amount, paymentData.OperationTitle));
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
            return new PaymentResponse(ResponseStatus.Success);
        }

        public PaymentResponse IncomingTransfer(TransferData transferData)
        {
            try
            {
                var account = GetAccount(transferData.AccountNumber);
                var amount = transferData.Amount/100m;
                ExecuteAndSave(account,
                    new IncomingTransfer(account, amount, transferData.Title, transferData.SenderAccountNumber));
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
            return new PaymentResponse(ResponseStatus.Success);
        }

        public PaymentResponse SoapTransfer(TransferData transferData, string accessToken) //transfer from soap
        {
           /* try
            {
                var sender = GetAccount(transferData.SenderAccountNumber, accessToken);
            }
            catch (BankException exception)
            {
                //not my account or access denied
                return new PaymentResponse(exception.ResponseStatus);
            }
            //access denied or unknown account bank exception
            var amount = transferData.Amount/100m;
            //TODO - check if sender has amount
            try
            {
                var receiver = GetAccount(transferData.AccountNumber);

            }
            catch (BankException e)
            {
                //not my account
                ExecuteAndSave(InterbankTransfer(sender, receiver, amount, transferData.Title));
                return new PaymentResponse(ResponseStatus.Success);
            }
            ExecuteAndSave(InnerTransfer(sender, receiver, amount, transferData.Title).execute);
            //sender has amount?*/
            return new PaymentResponse(ResponseStatus.Success);
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

        private void ExecuteAndSave(IAccount account, BankOperation operation)
        {
            operation.Execute();
            _dataInserter.SaveOperation(account, operation);
        }


        private void ExecuteAndSave(IPublicAccount account, BankOperation operation)
        {
            operation.Execute();
            _dataInserter.SaveOperation(account, operation);
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