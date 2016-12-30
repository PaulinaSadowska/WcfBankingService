﻿using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Database.SavingData
{
    public interface IBankDataInserter
    {
        void SaveAccessToken(string login, string accessToken);
        void SaveOperation(IAccount account, BankOperation operation);
    }
}