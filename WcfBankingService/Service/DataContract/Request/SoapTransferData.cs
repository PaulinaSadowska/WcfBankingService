using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Request
{
    [DataContract]
    public class SoapTransferData
    {
        [DataMember]
        public string Amount { get; set; }

        [DataMember]
        public string ReceiverAccountNumber { get; set; }

        [DataMember]
        public string SenderAccountNumber { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

    }
}