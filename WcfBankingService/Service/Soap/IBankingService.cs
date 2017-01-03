using System.ServiceModel;
using WcfBankingService.Service.DataContract;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.SoapService.DataContract.Response;

namespace WcfBankingService.Service.Soap
{
    [ServiceContract]
    public interface IBankingService
    {
        [OperationContract]
        LogInResponse SignIn(string login, string password);

        [OperationContract]
        PaymentResponse Deposit(DepositData paymentData);

        [OperationContract]
        PaymentResponse Withdraw(WithdrawData paymentData);

        [OperationContract]
        PaymentResponse Transfer(TransferData transferData, string accessToken);

        [OperationContract]
        OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber);

    }

}
