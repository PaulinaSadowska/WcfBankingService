﻿using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Database.SavingData
{
    public class MockDataInserter : IBankDataInserter
    {
        public void SaveAccessToken(string login, string accessToken)
        {

        }

        public void SaveOperation(BankOperation operation)
        {

        }
    }
}