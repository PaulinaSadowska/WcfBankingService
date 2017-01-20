using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Database.SavingData.Helper;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Database.SavingData
{
    /// <summary>
    /// <see cref="IBankDataInserter"/>
    /// </summary>
    public class DbDataInserter : IBankDataInserter
    {
        private readonly DataSaver _dataSaver;
        private readonly IndexesFinder _indexesFinder;

        public DbDataInserter()
        {
            _dataSaver = new DataSaver();
            _indexesFinder = new IndexesFinder();
        }

        /// <summary>
        /// <see cref="IBankDataInserter.SaveAccessToken"/>
        /// </summary>
        public void SaveAccessToken(string login, string accessToken)
        {
            var userId = _indexesFinder.GetUserId(login);
            _dataSaver.SaveToken(userId, accessToken);
        }

        /// <summary>
        /// <see cref="IBankDataInserter.SaveOperation"/>
        /// </summary>
        public void SaveOperation(BankOperation operation)
        {
            var accountId = _indexesFinder.GetAccountId(operation.AccountNumber.InnerNumber);
            _dataSaver.SaveOperationToHistory(accountId, operation.OperationRecord);
        }

        /// <summary>
        /// <see cref="IBankDataInserter.SaveOperations"/>
        /// </summary>
        public void SaveOperations(List<BankOperation> operations)
        {
            foreach (var operation in operations)
            {
                SaveOperation(operation);
            }
        }

        /// <summary>
        /// <see cref="IBankDataInserter.SaveAccountBalance"/>
        /// </summary>
        public void SaveAccountBalance(IPublicAccount account)
        {
            _dataSaver.SaveAccountBalance(account);
        }

    }
}