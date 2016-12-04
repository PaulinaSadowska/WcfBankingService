using System.Numerics;
using System.Runtime.Serialization;

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
        public BigInteger Amount { get; set; }

        [DataMember]
        public BigInteger BalanceAfterOperation { get; set; }
    }
}