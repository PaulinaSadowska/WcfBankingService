using System.ServiceModel;
using System.ServiceModel.Web;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.Service.Rest
{
    /// <summary>
    /// REST Service
    /// </summary>
    [ServiceContract]
    public interface IBankingRestService
    {
        /// <summary>
        /// incoming transfer operation (via REST)
        /// </summary>
        /// <param name="transferData">transfer data</param>
        /// <returns>response with possible error field</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
             BodyStyle = WebMessageBodyStyle.Bare,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "transfer")]
        TransferResponse Transfer(TransferData transferData);
    }
}