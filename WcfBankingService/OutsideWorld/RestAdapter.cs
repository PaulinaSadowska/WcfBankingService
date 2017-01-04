using RestSharp;
using RestSharp.Authenticators;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.OutsideWorld
{
    public class RestAdapter : IRestAdapter
    {
        private const string Endpoint = "transfer";
        private readonly AccountNumber _receiver;
        private readonly AccountNumber _sender;

        public RestAdapter(AccountNumber receiver, AccountNumber sender)
        {
            _receiver = receiver;
            _sender = sender;
        }

        public IRestResponse<BankRestResponse> Execute(decimal amountToSend, string operationTitle)
        {
            var client = new RestClient(GetBankAddress(_receiver.BankId));
            client.Authenticator = new SimpleAuthenticator("username", "admin", "password", "admin"); //TODO - read from config
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
                amount = (int)amountToSend * 100,
                receiver_account = _receiver.ToString(),
                sender_account = _sender.ToString(),
                title = operationTitle
            });
            return request;
        }

        private static string GetBankAddress(string receiverBankId)
        {
            return "https://github.com/PaulinaSadowska/"; // TODO read from config
        }
    }
}