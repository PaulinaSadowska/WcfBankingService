using System;
using System.Numerics;
using System.Runtime.Serialization;

namespace WcfBankingService.SOAPService.DataContract
{
    [DataContract]
    public class PaymentData
    {
        [DataMember]
        private string AccountNumber { get; set; }

        [DataMember]
        private string AccessToken { get; set; }

        [DataMember]
        private decimal Amount { get; set; }
    }
}