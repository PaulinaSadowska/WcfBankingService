using System.Runtime.Serialization;

namespace WcfBankingService.SoapService.DataContract.Response
{
    [DataContract]
    public class PaymentResponse : IResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; }

        public PaymentResponse(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
        }
    }
}