using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    /// <summary>
    /// response of transfer operation
    /// </summary>
    [DataContract]
    public class TransferResponse
    {
        /// <summary>
        /// message of error which occured during transfer (if it occurred)
        /// </summary>
        [DataMember(Name="error")]
        public string Error { get; set; }

        public TransferResponse()
        {
        }

        public TransferResponse(string errorMessage)
        {
            this.Error = errorMessage;
        }
    }
}