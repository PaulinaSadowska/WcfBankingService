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
            if (this != null) // TODO - how to know if operation performed correctly
            {
                ResponseStatus = ResponseStatus.Success;
                return;
            }
            ResponseStatus = ResponseStatus.InsufficientFunds;
        }
    }
}