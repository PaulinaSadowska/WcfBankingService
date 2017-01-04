using System.Runtime.Serialization;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.operation
{
    [DataContract]
    public class OperationRecord
    {
        [DataMember]
        public  string Source { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public decimal BalanceAfterOperation { get; set; }
    }
}