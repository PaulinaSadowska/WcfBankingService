using System.Collections.Generic;
using System.Security.Authentication;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Accounts.Number.ControlSum;
using WcfBankingService.operation;
using WcfBankingService.operation.operations;
using WcfBankingService.Operation.Operations;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;
using WcfBankingService.Users;

namespace WcfBankingService
{
    public class Bank
    {
        private const string BankId = "11216900";

        private readonly UserManager _userManager;
        private readonly AccountNumberFactory _accountNumberFactory;

        public Bank()
        {
            _userManager = new UserManager();
            _accountNumberFactory = new AccountNumberFactory(BankId, new StandardControlSumCalculator());
        }

        public string SignIn(string login, string password)
        {
            return _userManager.SignIn(login, password);
        }

        public ResponseStatus Deposit(PaymentData paymentData)
        {
            try
            {
                new Deposit(GetAccount(paymentData.AccessToken, paymentData.AccountNumber), paymentData.Amount,
                    paymentData.OperationTitle).Execute();
            }
            catch (BankException exception)
            {
                return exception.ResponseStatus;
            }
            return ResponseStatus.Success;
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

        public ResponseStatus Withdraw(PaymentData paymentData)
        {
            try
            {
                new Withdraw(GetAccount(paymentData.AccessToken, paymentData.AccountNumber), 
                    paymentData.Amount, paymentData.OperationTitle).Execute();
            }
            catch (BankException exception)
            {
                return exception.ResponseStatus;
            }
            return ResponseStatus.Success;
            
        }

        public IEnumerable<OperationRecord> GetOperationHistory(string accessToken, string accountNumber)
        {
            var account = _userManager.GetAccount(accessToken, _accountNumberFactory.GetAccountNumber(accountNumber));
            return account.GetOperationHistory();
        }
    }
}