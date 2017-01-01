using System.ServiceModel;
using System.ServiceModel.Web;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;

namespace WcfBankingService.RestService
{
    [ServiceContract]
    public interface IBankingRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
             BodyStyle = WebMessageBodyStyle.Bare,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "transfer")]
        string Transfer(TransferData transferData);
    }
}