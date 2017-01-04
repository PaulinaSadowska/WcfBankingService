using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

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

        public void SaveOperations(List<BankOperation> operations)
        {
        }

        public void SaveAccountBalance(IPublicAccount account)
        {
        }

        public void SaveOperation(IPublicAccount account, BankOperation operation)
        {
        }
    }
}