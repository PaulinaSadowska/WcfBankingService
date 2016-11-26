using System;
using System.Collections.Generic;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService
{
    public class BankingService : IBankingService
    {

        public OperationResponse signIn(string login, string password)
        {
            return OperationResponse.Success;
        }
        
        public OperationResponse deposit(PaymentData paymentData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Operation> getOperationHistory(String accountNumber)
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
