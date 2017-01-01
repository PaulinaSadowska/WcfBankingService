using System.ServiceModel;
using System.ServiceModel.Web;
using WcfBankingService.Service.DataContract;
using WcfBankingService.Service.DataContract.Request;

namespace WcfBankingService.Service.Rest
{
    [ServiceContract]
    public interface IBankingRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
             BodyStyle = WebMessageBodyStyle.Bare,
           //  RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "transfer")]
        TransferResponse Transfer(TransferData transferData);
    }
}