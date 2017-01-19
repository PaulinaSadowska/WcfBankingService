using System.ServiceModel;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.Service.Soap
{
    /// <summary>
    /// SOAP Service
    /// </summary>
    [ServiceContract]
    public interface IBankingService
    {
        /// <summary>
        /// Sign in operation
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="password">user password</param>
        /// <returns>log in operation response</returns>
        [OperationContract]
        LogInResponse SignIn(string login, string password);

        /// <summary>
        /// Money deposit operation
        /// </summary>
        /// <param name="paymentData">data needed to perform deposit</param>
        /// <returns>operation status</returns>
        [OperationContract]
        PaymentResponse Deposit(DepositData paymentData);

        /// <summary>
        /// Money withdraw operation
        /// </summary>
        /// <param name="paymentData">data needed to perform withdraw</param>
        /// <returns>operation status</returns>
        [OperationContract]
        PaymentResponse Withdraw(WithdrawData paymentData);

        /// <summary>
        /// Money transfer operation
        /// </summary>
        /// <param name="transferData">data needed to perform transfer</param>
        /// <returns>operation status</returns>
        [OperationContract]
        PaymentResponse Transfer(SoapTransferData transferData);

        /// <summary>
        /// Fetches operation history from account with given number
        /// </summary>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <param name="accountNumber">account number</param>
        /// <returns>response status and operation history</returns>
        [OperationContract]
        OperationHistoryResponse GetOperationHistory(string accessToken, string accountNumber);

    }

}
