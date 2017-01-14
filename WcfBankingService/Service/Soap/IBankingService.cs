using System.ServiceModel;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;

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
        PaymentResponse Transfer(SoapTransferData transferData);

        [OperationContract]
        OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber);

    }

}
