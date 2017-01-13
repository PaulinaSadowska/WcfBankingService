using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    [DataContract]
    public class PaymentResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        public PaymentResponse(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
        }
    }
}