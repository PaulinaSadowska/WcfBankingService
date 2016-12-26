using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfBankingService.SoapService.DataContract.Response
{
    [DataContract]
    public class LogInResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; }

        public LogInResponse(string accessToken)
        {
            if (accessToken != null)
            {
                ResponseStatus = ResponseStatus.Success;
                return;
            }
            ResponseStatus = ResponseStatus.IncorrectLoginOrPassword;
        }
    }
}