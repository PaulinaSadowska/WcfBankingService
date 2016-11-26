using System.Runtime.Serialization;

namespace WcfBankingService
{
    [DataContract]
    public class OperationStatus
    {
        /// Response Code
        /// 0 - success
        /// other - errorCodes
        [DataMember]
        int ResponseCode { get; set; }

        /// readable error message
        [DataMember]
        string ErrorMessage { get; set; }
    }
}