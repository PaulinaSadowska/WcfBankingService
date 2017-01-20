using System;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService
{
    /// <summary>
    /// Exception throws by Bank classes when bank error occurred
    /// </summary>
    public class BankException : Exception
    {
        /// <summary>
        /// status of the response
        /// </summary>
        public ResponseStatus ResponseStatus { get; }

        /// <summary>
        /// Defualt constructor
        /// </summary>
        /// <param name="responseStatus">status of the response</param>
        public BankException(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
        }
    }
}