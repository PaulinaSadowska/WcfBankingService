using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    /// <summary>
    /// response after withdraw and deposit operations
    /// </summary>
    [DataContract]
    public class PaymentResponse
    {
        /// <summary>
        /// response status
        /// </summary>
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        public PaymentResponse(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
        }
    }
}