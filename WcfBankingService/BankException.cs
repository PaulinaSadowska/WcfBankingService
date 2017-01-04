using System;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.SoapService.DataContract.Response;

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