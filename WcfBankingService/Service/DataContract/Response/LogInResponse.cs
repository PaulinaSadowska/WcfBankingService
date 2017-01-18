using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    [DataContract]
    public class LogInResponse
    {
        [DataMember]
        public string AccessToken { get; set; }

        [DataMember]
        public List<string> AccountNumbers { get; set; }

        public LogInResponse(string accessToken, List<string> accountNumbers)
        {
            AccessToken = accessToken;
            AccountNumbers = accountNumbers;
        }
    }
}