using System;
using System.Collections.Generic;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Accounts.Number.ControlSum;
using WcfBankingService.operation;
using WcfBankingService.operation.operations;
using WcfBankingService.Operation.Operations;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SoapService.Validation;
using WcfBankingService.SOAPService.DataContract;
using WcfBankingService.Users;

namespace WcfBankingService.SoapService
{
    public class BankingService : IBankingService
    {
        private readonly IServiceInputValidator _inputValidator;
        private readonly UserManager _userManager;
        private readonly AccountNumberFactory _accountNumberFactory;

        public BankingService()
        {
            _inputValidator = new ServiceInputValidator();
            _userManager = new UserManager();
            _accountNumberFactory = new AccountNumberFactory("11216900", new StandardControlSumCalculator());
        }

        public LogInResponse SignIn(string login, string password)
        {
            _inputValidator.CheckSignInDataValid(login, password);
            return new LogInResponse(_userManager.SignIn(login, password));
        }

        public PaymentResponse Deposit(PaymentData paymentData)
        {
            _inputValidator.CheckPaymentData(paymentData);
            var account = _userManager.GetAccount(paymentData.AccessToken, _accountNumberFactory.GetAccountNumber(paymentData.AccountNumber));
            new Deposit(account, paymentData.Amount, paymentData.OperationTitle).Execute();
            return new PaymentResponse();
        }

        public PaymentResponse Transfer(TransferData transferData)
        {
            throw new NotImplementedException();
        }

        public PaymentResponse Withdraw(PaymentData paymentData)
        {
            _inputValidator.CheckPaymentData(paymentData);
            var account = _userManager.GetAccount(paymentData.AccessToken, _accountNumberFactory.GetAccountNumber(paymentData.AccountNumber));
            new Withdraw(account, paymentData.Amount, paymentData.OperationTitle).Execute();
            return new PaymentResponse();
        }

        public IEnumerable<OperationRecord> GetOperationHistory(string accessToken, string accountNumber)
        {
           //TODO - validate account number
            var account = _userManager.GetAccount(accessToken, _accountNumberFactory.GetAccountNumber(accountNumber));
            return account.GetOperationHistory();
            //TODO - change type to OperationHistoryResponse to send error codes
        }
    }
}
