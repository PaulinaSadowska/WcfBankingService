using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Request
{
    [DataContract]
    public class DepositData
    {
        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string OperationTitle { get; set; }
    }
}
