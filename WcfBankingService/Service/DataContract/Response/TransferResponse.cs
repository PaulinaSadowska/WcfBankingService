using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    [DataContract]
    public class TransferResponse
    {
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