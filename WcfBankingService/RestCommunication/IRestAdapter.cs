using RestSharp;

namespace WcfBankingService.RestCommunication
{
    /// <summary>
    /// allows to create and execute transfer request via REST
    /// </summary>
    public interface IRestAdapter
    {
        /// <summary>
        /// Executes REST transfer request to another bank
        /// </summary>
        /// <param name="amount">amount to send</param>
        /// <param name="operationTitle">operation title</param>
        /// <returns>rest response</returns>
        IRestResponse<BankRestResponse> Execute(decimal amount, string operationTitle);
    }
}