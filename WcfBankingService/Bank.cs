using System.Collections.Generic;
using System.Security.Authentication;
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
            var accountNumber = _accountNumberFactory.GetAccountNumber(paymentData.AccountNumber);
            if(accountNumber==null)
                return ResponseStatus.WrongAccountNumber;
            try
            {
                var account = _userManager.GetAccount(paymentData.AccessToken, accountNumber);
                if (account == null)
                {
                    return ResponseStatus.AccountNumberDoesntExist;
                }
                new Deposit(account, paymentData.Amount, paymentData.OperationTitle).Execute();
                return ResponseStatus.Success;
            }
            catch (AuthenticationException)
            {
                return ResponseStatus.AccessDenied;
            }
        }

        public void Withdraw(PaymentData paymentData)
        {
            var account = _userManager.GetAccount(paymentData.AccessToken, _accountNumberFactory.GetAccountNumber(paymentData.AccountNumber));
            new Withdraw(account, paymentData.Amount, paymentData.OperationTitle).Execute();
        }

        public IEnumerable<OperationRecord> GetOperationHistory(string accessToken, string accountNumber)
        {
            var account = _userManager.GetAccount(accessToken, _accountNumberFactory.GetAccountNumber(accountNumber));
            return account.GetOperationHistory();
        }
    }
}