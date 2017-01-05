using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Database.SavingData.Helper;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Database.SavingData
{
    public class DbDataInserter : IBankDataInserter
    {
        private readonly DataSaver _dataSaver;
        private readonly IndexesFinder _indexesFinder;

        public DbDataInserter()
        {
            _dataSaver = new DataSaver();
            _indexesFinder = new IndexesFinder();
        }

        public void SaveAccessToken(string login, string accessToken)
        {
            var userId = _indexesFinder.GetUserId(login);
            _dataSaver.SaveToken(userId, accessToken);
        }

        public void SaveOperation(BankOperation operation)
        {
            var accountId = _indexesFinder.GetAccountId(operation.AccountNumber.InnerNumber);
            _dataSaver.SaveOperationToHistory(accountId, operation.OperationRecord);
        }

        public void SaveOperations(List<BankOperation> operations)
        {
            foreach (var operation in operations)
            {
                SaveOperation(operation);
            }
        }


        public void SaveAccountBalance(IPublicAccount account)
        {
            _dataSaver.SaveAccountBalance(account);
        }

    }
}