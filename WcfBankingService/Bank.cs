using System.Security.Authentication;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Accounts.Number.ControlSum;
using WcfBankingService.Database.DataProvider;
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
            _accountNumberFactory = new AccountNumberFactory(BankId, new StandardControlSumCalculator());
            _userManager = new UserManager(new DbDataProvider(_accountNumberFactory));
        }

        public LogInResponse SignIn(string login, string password)
        {
            return new LogInResponse(_userManager.SignIn(login, password));
        }

        public PaymentResponse Deposit(PaymentData paymentData)
        {
            try
            {
                new Deposit(GetAccount(paymentData.AccessToken, paymentData.AccountNumber), paymentData.Amount,
                    paymentData.OperationTitle).Execute();
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
            return new PaymentResponse(ResponseStatus.Success);
        }

        public PaymentResponse Withdraw(PaymentData paymentData)
        {
            try
            {
                new Withdraw(GetAccount(paymentData.AccessToken, paymentData.AccountNumber),
                    paymentData.Amount, paymentData.OperationTitle).Execute();
            }
            catch (BankException exception)
            {
                return new PaymentResponse(exception.ResponseStatus);
            }
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
    }
}