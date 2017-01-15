using RestSharp;

namespace WcfBankingService.RestCommunication
{
    public interface IRestAdapter
    {
        IRestResponse<BankRestResponse> Execute(decimal amount, string operationTitle);
    }
}