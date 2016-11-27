using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService
{
    [ServiceContract]
    public interface IBankingService
    {
        [OperationContract]
        OperationResponse signIn(String login, String password);

        [OperationContract]
        IEnumerable<OperationRecord> getOperationHistory(String accountNumber);

        [OperationContract]
        OperationResponse transfer(TransferData transferData);

        [OperationContract]
        OperationResponse deposit(PaymentData paymentData);

        [OperationContract]
        OperationResponse withdraw(PaymentData paymentData);

    }

}
