using System.Runtime.Serialization;

namespace WcfBankingService.SOAPService.DataContract
{
    [DataContract]
    public class PaymentData
    {
        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

        [DataMember]
         public decimal Amount { get; set; }

        [DataMember]
        public string OperationTitle { get; set; }
    }
}