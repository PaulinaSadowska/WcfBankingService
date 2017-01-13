using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    [DataContract]
    public class LogInResponse
    {
        [DataMember]
        public string AccessToken { get; set; }

        public LogInResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}