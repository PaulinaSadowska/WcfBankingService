using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfBankingService.RestService
{
    [ServiceContract]
    public interface IBankingRestService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "json/{id}")]
        Output JsonData(string id);
    }
}
