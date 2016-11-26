using System;
using System.Collections.Generic;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService
{
    public class BankingService : IBankingService
    {

        public OperationStatus signIn(string login, string password)
        {
            throw new NotImplementedException();
        }
        
        public OperationStatus deposit(PaymentData paymentData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Operation> getOperationHistory(String accountNumber)
        {
            throw new NotImplementedException();
        }

        public OperationStatus transfer(TransferData transferData)
        {
            throw new NotImplementedException();
        }

        public OperationStatus withdraw(PaymentData paymentData)
        {
            throw new NotImplementedException();
        }
    }
}
