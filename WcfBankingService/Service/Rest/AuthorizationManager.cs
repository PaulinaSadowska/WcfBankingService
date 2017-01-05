using System;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Configuration;

namespace WcfBankingService.Service.Rest
{
    public class AuthorizationManager : ServiceAuthorizationManager
    {
        private readonly string _login = WebConfigurationManager.AppSettings["BasicAuthLogin"];
        private readonly string _password = WebConfigurationManager.AppSettings["BasicAuthPassword"];


        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            if (WebOperationContext.Current == null) return false;

            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var svcCredentials = GetCredentialsFromHeader(authHeader);
                var user = new {Name = svcCredentials[0], Password = svcCredentials[1]};
                if (user.Name == _login && user.Password == _password)
                {
                    return true;
                }
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Forbidden;
                return false;
            }
            PromptBasicAuth();
            return false;
        }

        private static string[] GetCredentialsFromHeader(string authHeader)
        {
            return System.Text.Encoding.ASCII
                    .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                    .Split(':');
        }

        private static void PromptBasicAuth()
        {
            if (WebOperationContext.Current == null) throw new WebFaultException(HttpStatusCode.Unauthorized);

            WebOperationContext.Current.OutgoingResponse.Headers.Add(
                "WWW-Authenticate: Basic realm=\"WcfRestDemo\"");
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
            throw new WebFaultException(HttpStatusCode.Unauthorized);
        }
    }
}