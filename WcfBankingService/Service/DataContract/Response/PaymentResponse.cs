using System.Runtime.Serialization;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.SoapService.DataContract.Response
{
    [DataContract]
    public class PaymentResponse : IResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        public PaymentResponse(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
        }
    }
}