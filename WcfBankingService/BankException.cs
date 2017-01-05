using System;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService
{
    public class BankException : Exception
    {
        public ResponseStatus ResponseStatus { get; }

        public BankException(ResponseStatus responseStatus)
        {
            ResponseStatus = responseStatus;
        }
    }
}