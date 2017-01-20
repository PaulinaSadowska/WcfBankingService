using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    /// <summary>
    /// response of log in operation
    /// </summary>
    [DataContract]
    public class LogInResponse
    {
        /// <summary>
        /// user access token, needed to perform operations on his accounts
        /// </summary>
        [DataMember]
        public string AccessToken { get; set; }

        /// <summary>
        /// user account numbers list
        /// </summary>
        [DataMember]
        public List<string> AccountNumbers { get; set; }

        public LogInResponse(string accessToken, List<string> accountNumbers)
        {
            AccessToken = accessToken;
            AccountNumbers = accountNumbers;
        }
    }
}