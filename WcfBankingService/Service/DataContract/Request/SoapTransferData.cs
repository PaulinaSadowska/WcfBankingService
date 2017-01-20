using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Request
{
    /// <summary>
    /// Data needed to perform soap transfer
    /// </summary>
    [DataContract]
    public class SoapTransferData
    {
        /// <summary>
        /// amount to transfer (not negative, x.xx precision)
        /// </summary>
        [DataMember]
        public string Amount { get; set; }

        /// <summary>
        /// receuver account number (full, 26 digits)
        /// </summary>
        [DataMember]
        public string ReceiverAccountNumber { get; set; }

        /// <summary>
        /// sender account number (full, 26 digits)
        /// </summary>
        [DataMember]
        public string SenderAccountNumber { get; set; }

        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// user's access token, sent after log in
        /// </summary>
        [DataMember]
        public string AccessToken { get; set; }

    }
}