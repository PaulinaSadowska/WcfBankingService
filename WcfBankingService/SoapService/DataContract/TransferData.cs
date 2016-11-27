using System.Runtime.Serialization;

namespace WcfBankingService.SOAPService.DataContract
{
    [DataContract]
    public class TransferData : PaymentData
    {
        [DataMember]
        private string SenderAccountNumber { get; set; }

        [DataMember]
        private string Title { get; set; }

    }
}