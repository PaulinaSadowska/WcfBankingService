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
        OperationStatus signIn(String login, String password);

        [OperationContract]
        IEnumerable<Operation> getOperationHistory(String accountNumber);

        [OperationContract]
        OperationStatus transfer(TransferData transferData);

        [OperationContract]
        OperationStatus deposit(PaymentData paymentData);

        [OperationContract]
        OperationStatus withdraw(PaymentData paymentData);

    }

}
