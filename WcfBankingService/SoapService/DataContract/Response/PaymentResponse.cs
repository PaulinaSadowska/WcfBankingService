using System.Runtime.Serialization;

namespace WcfBankingService.SoapService.DataContract.Response
{
    [DataContract]
    public class PaymentResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; }

        public PaymentResponse()
        {
            ResponseStatus = ResponseStatus.Success;
            //ResponseStatus = ResponseStatus.InsufficientFunds; //TODO
        }
    }
}