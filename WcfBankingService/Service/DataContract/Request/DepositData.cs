using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Request
{
    /// <summary>
    /// data needed to perform deposit
    /// </summary>
    [DataContract]
    public class DepositData
    {
        /// <summary>
        /// account number (full, 26 numbers)
        /// </summary>
        [DataMember]
        public string AccountNumber { get; set; }

        /// <summary>
        /// amount to deposit (not negative, x.xx precision)
        /// </summary>
        [DataMember]
        public string Amount { get; set; }

        /// <summary>
        /// operation title
        /// </summary>
        [DataMember]
        public string OperationTitle { get; set; }
    }
}
