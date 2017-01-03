using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Database.SavingData
{
    public interface IBankDataInserter
    {
        void SaveAccessToken(string login, string accessToken);
        void SaveOperation(BankOperation operation);
        void SaveOperations(List<BankOperation> operations);
        void SaveAccountBalance(IPublicAccount account);
    }
}
