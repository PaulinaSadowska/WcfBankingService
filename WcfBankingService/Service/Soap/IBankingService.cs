using System.ServiceModel;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.Service.Soap
{
    [ServiceContract]
    public interface IBankingService
    {
        [OperationContract]
        LogInResponse SignIn(string login, string password);

        [OperationContract]
        PaymentResponse Deposit(PaymentData paymentData);

        [OperationContract]
        PaymentResponse Withdraw(PaymentData paymentData);

        [OperationContract]
        PaymentResponse Transfer(TransferData transferData);

        [OperationContract]
        OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber);

    }

}
