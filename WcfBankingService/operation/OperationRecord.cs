using System.Runtime.Serialization;

namespace WcfBankingService.operation
{
    /// <summary>
    /// stores information about operation record
    /// </summary>
    [DataContract]
    public class OperationRecord
    {
        /// <summary>
        /// source of the money (operation name or sender account number)
        /// </summary>
        [DataMember]
        public  string Source { get; set; }

        /// <summary>
        /// operation title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// debet value
        /// </summary>
        [DataMember]
        public decimal Debet { get; set; }

        /// <summary>
        /// credit value
        /// </summary>
        [DataMember]
        public decimal Credit { get; set; }

        /// <summary>
        /// balance after operation
        /// </summary>
        [DataMember]
        public decimal BalanceAfterOperation { get; set; }
    }
}