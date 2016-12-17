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
        ResponseStatus Transfer(TransferData transferData);

        [OperationContract]
        ResponseStatus Deposit(PaymentData paymentData);

        [OperationContract]
        ResponseStatus Withdraw(PaymentData paymentData);

    }

}
