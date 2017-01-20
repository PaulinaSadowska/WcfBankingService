using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Request
{
    /// <summary>
    /// Data needed to perform rest transfer
    /// </summary>
    [DataContract]
    public class TransferData 
    {
        /// <summary>
        /// <see cref="SoapTransferData.Amount"/>
        /// </summary>
        [DataMember(Name="amount")]
        public int Amount { get; set; }

        /// <summary>
        /// <see cref="SoapTransferData.SenderAccountNumber"/>
        /// </summary>
        [DataMember(Name = "receiver_account")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// <see cref="SoapTransferData.ReceiverAccountNumber"/>
        /// </summary>
        [DataMember(Name = "sender_account")]
        public string SenderAccountNumber { get; set; }

        /// <summary>
        /// <see cref="SoapTransferData.AccessToken"/>
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

    }
}