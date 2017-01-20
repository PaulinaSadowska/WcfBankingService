using System.Web.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using WcfBankingService.Accounts.Number;
using ResponseStatus = WcfBankingService.Service.DataContract.Response.ResponseStatus;

namespace WcfBankingService.RestCommunication
{
    /// <summary>
    /// <see cref="IRestAdapter"/>
    /// </summary>
    public class RestAdapter : IRestAdapter
    {
        private const string Endpoint = "transfer";
        private readonly AccountNumber _receiver;
        private readonly AccountNumber _sender;

        /// <summary>
        /// creates rest adapter to execute rest request
        /// </summary>
        /// <param name="receiver">receiver full account number (from other bank)</param>
        /// <param name="sender">sender full account number (from this bank)</param>
        public RestAdapter(AccountNumber receiver, AccountNumber sender)
        {
            _receiver = receiver;
            _sender = sender;
        }

        /// <summary>
        /// <see cref="IRestAdapter.Execute"/>
        /// crteates rest client with basic auth credentials and executes request
        /// </summary>
        public IRestResponse<BankRestResponse> Execute(decimal amountToSend, string operationTitle)
        {
            var basicAuthLogin = WebConfigurationManager.AppSettings["BasicAuthLogin"];
            var basicAuthPassword = WebConfigurationManager.AppSettings["BasicAuthPassword"];
            var client = new RestClient(GetBankAddress(_receiver.BankId))
            {
                Authenticator = new HttpBasicAuthenticator(basicAuthLogin, basicAuthPassword)
            };
            return client.Execute<BankRestResponse>(CreateRequest(amountToSend, operationTitle));
        }

        private RestRequest CreateRequest(decimal amountToSend, string operationTitle)
        {
            var request = new RestRequest(Endpoint, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(new RestTransferBody()
            {
                amount = (int) amountToSend*100,
                receiver_account = _receiver.ToString(),
                sender_account = _sender.ToString(),
                title = operationTitle
            });
            return request;
        }

        private static string GetBankAddress(string receiverBankId)
        {
            var bankAddress = WebConfigurationManager.AppSettings[receiverBankId];
            if(bankAddress == null)
                throw new BankException(ResponseStatus.BankNotExists);
            return bankAddress;
        }
    }
}