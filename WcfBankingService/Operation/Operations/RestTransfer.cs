﻿using System.Net;
using RestSharp;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;
using WcfBankingService.RestCommunication;
using ResponseStatus = WcfBankingService.Service.DataContract.Response.ResponseStatus;

namespace WcfBankingService.Operation.Operations
{
    /// <summary>
    /// Transfer performed between banks connected via REST
    /// </summary>
    public class RestTransfer: BankOperation
    {
        private readonly IRestAdapter _restAdapter;
        private readonly decimal _amount;
        private readonly string _operationTitle;

        /// <param name="sender">sender account number</param>
        /// <param name="amount">amount to transfer</param>
        /// <param name="operationTitle">operation title</param>
        /// <param name="receiver">receiver  account number</param>
        public RestTransfer(AccountNumber sender, decimal amount, 
            string operationTitle, AccountNumber receiver) 
            : base(receiver, new OperationRecord
            {
                Title = operationTitle,
                Debet = amount,
                Source = receiver.ToString()
            })
        {
            _restAdapter = new RestAdapter(receiver, sender);
            _amount = amount;
            _operationTitle = operationTitle;
        }

        /// <summary>
        /// execute Rest transaction. Throws exception when respose contains error.
        /// </summary>
        public override void Execute()
        {
            ValidateResponse(_restAdapter.Execute(_amount, _operationTitle));
        }

        private static void ValidateResponse(IRestResponse<BankRestResponse> response)
        {
            if(response.StatusCode != HttpStatusCode.Created)
                throw new BankException(ResponseStatus.InterbankTransferFailed);
        }
    }
}