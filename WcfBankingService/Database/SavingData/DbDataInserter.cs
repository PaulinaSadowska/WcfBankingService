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

        public void SaveOperation(IPublicAccount account, BankOperation operation)
        {
            _dataSaver.SaveAccountBalance(account);
            var accountId = _indexesFinder.GetAccountId(account.AccountNumber.InnerNumber);
            _dataSaver.SaveOperationToHistory(accountId, operation.OperationRecord);
        }
    }
}