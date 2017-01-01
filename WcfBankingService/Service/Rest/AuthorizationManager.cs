using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfBankingService.Service.Rest
{
    public class AuthorizationManager : ServiceAuthorizationManager
    {
        private const string Login = "Admin";
        private const string Password = "Pass";

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            if (WebOperationContext.Current == null) return false;

            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var svcCredentials = GetCredentialsFromHeader(authHeader);
                var user = new {Name = svcCredentials[0], Password = svcCredentials[1]};
                if (user.Name == Login && user.Password == Password)
                {
                    return true;
                }
                PromptBasicAuth();
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