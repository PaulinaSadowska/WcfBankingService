using RestSharp;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.OutsideWorld
{
    public interface IRestAdapter
    {
        IRestResponse<BankRestResponse> Execute(decimal amount, string operationTitle);
    }
}