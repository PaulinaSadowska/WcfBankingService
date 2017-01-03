﻿using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Operation.Operations
{
    public class RestTransfer: BankOperation
    {
        public RestTransfer(IAccount sender, decimal amount, 
            string operationTitle, AccountNumber receiverAccountNumber) : 
            base(operationTitle, amount, "")
        {
            //TODO - implement
        }

        public override void Execute()
        {
            //TODO - implement
        }
    }
}