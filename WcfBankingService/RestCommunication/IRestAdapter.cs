using RestSharp;

namespace WcfBankingService.RestCommunication
{
    public interface IRestAdapter
    {
        /// <summary>
        /// Executes REST request to another bank
        /// </summary>
        /// <param name="amount">amount to send</param>
        /// <param name="operationTitle">operation title</param>
        /// <returns>rest response</returns>
        IRestResponse<BankRestResponse> Execute(decimal amount, string operationTitle);
    }
}