using System.Net;
using RestSharp;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.OutsideWorld;
using ResponseStatus = WcfBankingService.Service.DataContract.Response.ResponseStatus;

namespace WcfBankingService.Operation.Operations
{
    public class RestTransfer: BankOperation
    {
        private readonly IRestAdapter _restAdapter;
        private readonly decimal _amount;
        private readonly string _operationTitle;

        public RestTransfer(AccountNumber sender, decimal amount, 
            string operationTitle, AccountNumber receiver) 
            : base(receiver, operationTitle, amount, $"RestTransfer to {receiver}")
        {
            _restAdapter = new RestAdapter(receiver, sender);
            _amount = amount;
            _operationTitle = operationTitle;
        }

        public override void Execute()
        {
            ValidateResponse(_restAdapter.Execute(_amount, _operationTitle));
        }

        private static void ValidateResponse(IRestResponse<BankRestResponse> response)
        {
            if (!(response?.StatusCode == HttpStatusCode.Created || response?.Data.error == null))
                throw new BankException(ResponseStatus.InterbankTransferFailed);
        }
    }
}