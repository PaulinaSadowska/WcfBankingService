using System.Collections.Generic;
using System.ServiceModel;
using WcfBankingService.operation;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.SoapService
{
    [ServiceContract]
    public interface IBankingService
    {
        [OperationContract]
        LogInResponse SignIn(string login, string password);

        [OperationContract]
        IEnumerable<OperationRecord> GetOperationHistory(string accountNumber);

        [OperationContract]
        PaymentResponse Transfer(TransferData transferData);

        [OperationContract]
        PaymentResponse Deposit(PaymentData paymentData);

        [OperationContract]
        PaymentResponse Withdraw(PaymentData paymentData);

    }

}
