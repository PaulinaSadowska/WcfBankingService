using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfBankingService.SoapService;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService
{
    public class BankingService : IBankingService
    {
        private IServiceInputValidator InputValidator;

        public BankingService()
        {
            InputValidator = new ServiceInputValidator();
        }

        public OperationResponse signIn(string login, string password)
        {
            InputValidator.CheckSignInDataValid(login, password);
            return OperationResponse.Success;
        }
        
        public OperationResponse deposit(PaymentData paymentData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationRecord> getOperationHistory(String accountNumber)
        {
            throw new NotImplementedException();
        }

        public OperationResponse transfer(TransferData transferData)
        {
            throw new NotImplementedException();
        }

        public OperationResponse withdraw(PaymentData paymentData)
        {
            throw new NotImplementedException();
        }
    }
}
