using System.Numerics;
using System.Runtime.Serialization;

namespace WcfBankingService.SOAPService.DataContract
{
    [DataContract]
    public class Operation
    {
        [DataMember]
        private string OperationTitle { get; set; }

        [DataMember]
        private BigInteger Amount { get; set; }

        [DataMember]
        private MoneySource MoneySource { get; set; }

        [DataMember]
        private BigInteger BalanceAfterOperation { get; set; }
    }
}