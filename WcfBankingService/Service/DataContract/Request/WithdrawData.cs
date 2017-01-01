using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Request
{
    [DataContract]
    public class WithdrawData
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