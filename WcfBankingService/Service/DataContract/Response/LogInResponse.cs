using System.Runtime.Serialization;

namespace WcfBankingService.Service.DataContract.Response
{
    [DataContract]
    public class LogInResponse : IResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

        public LogInResponse(string accessToken)
        {
            if (accessToken != null)
            {
                ResponseStatus = ResponseStatus.Success;
                AccessToken = accessToken;
                return;
            }
            ResponseStatus = ResponseStatus.IncorrectLoginOrPassword;
        }

        public LogInResponse(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus != ResponseStatus.Success ? responseStatus : ResponseStatus.AccessDenied;
        }
    }
}