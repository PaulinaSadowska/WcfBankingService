using System;
using System.Collections.Generic;
using WcfBankingService.operation;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SoapService.Validation;
using WcfBankingService.SOAPService.DataContract;
using WcfBankingService.User;

namespace WcfBankingService.SoapService
{
    public class BankingService : IBankingService
    {
        private readonly IServiceInputValidator _inputValidator;
        private readonly UserManager _userManager;

        public BankingService()
        {
            _inputValidator = new ServiceInputValidator();
            _userManager = new UserManager();
        }

        public LogInResponse SignIn(string login, string password)
        {
            _inputValidator.CheckSignInDataValid(login, password);
            return new LogInResponse(_userManager.SignIn(login, password));
        }
        
        public ResponseStatus Deposit(PaymentData paymentData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationRecord> GetOperationHistory(String accountNumber)
        {
            throw new NotImplementedException();
        }

        public ResponseStatus Transfer(TransferData transferData)
        {
            throw new NotImplementedException();
        }

        public ResponseStatus Withdraw(PaymentData paymentData)
        {
            throw new NotImplementedException();
        }
    }
}
