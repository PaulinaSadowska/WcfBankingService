using System.Runtime.Serialization;

namespace WcfBankingService.SOAPService.DataContract
{
    [DataContract]
    public class TransferData 
    {
        [DataMember(Name="amount")]
        public int Amount { get; set; }

        [DataMember(Name = "receiver_account")]
        public string AccountNumber { get; set; }

        [DataMember(Name = "sender_account")]
        public string SenderAccountNumber { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

    }
}