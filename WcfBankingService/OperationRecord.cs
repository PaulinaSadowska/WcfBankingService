using System.Numerics;
using System.Runtime.Serialization;

namespace WcfBankingService.SOAPService.DataContract
{
    [DataContract]
    public class OperationRecord
    {
        //TODO - add information if it is withdraw, deposit or transfer
        [DataMember]
        private string OperationTitle { get; set; }

        [DataMember]
        private BigInteger Amount { get; set; }

        [DataMember]
        private BigInteger BalanceAfterOperation { get; set; }
    }
}