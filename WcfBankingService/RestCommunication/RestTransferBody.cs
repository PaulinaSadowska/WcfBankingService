// ReSharper disable InconsistentNaming

namespace WcfBankingService.RestCommunication
{
    /// <summary>
    /// body of transfer REST operation
    /// </summary>
    public class RestTransferBody
    {
        /// <summary>
        /// amount to transfer
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// sender account number
        /// </summary>
        public string sender_account { get; set; }

        /// <summary>
        /// receiver account number
        /// </summary>
        public string receiver_account { get; set; }

        /// <summary>
        /// transfers title
        /// </summary>
        public string title { get; set; }
    }
}