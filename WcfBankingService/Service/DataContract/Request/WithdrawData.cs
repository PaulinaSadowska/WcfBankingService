using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Request
{
    /// <summary>
    /// data needed to perfor withdraw operation
    /// </summary>
    [DataContract]
    public class WithdrawData
    {
        /// <summary>
        /// <see cref="DepositData.AccountNumber"/>
        /// </summary>
        [DataMember]
        public string AccountNumber { get; set; }

        /// <summary>
        /// user's access token, sent after log in
        /// </summary>
        [DataMember]
        public string AccessToken { get; set; }

        /// <summary>
        /// <see cref="DepositData.Amount"/>
        /// </summary>
        [DataMember]
         public string Amount { get; set; }

        /// <summary>
        /// <see cref="DepositData.OperationTitle"/>
        /// </summary>
        [DataMember]
        public string OperationTitle { get; set; }
    }
}