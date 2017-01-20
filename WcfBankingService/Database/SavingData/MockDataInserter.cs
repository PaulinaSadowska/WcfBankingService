using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Database.SavingData
{
    /// <summary>
    /// mocks data inserter functionality, used for tests
    /// </summary>
    public class MockDataInserter : IBankDataInserter
    {
        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="login">anything (ignored)</param>
        /// <param name="accessToken">anything (ignored)</param>
        public void SaveAccessToken(string login, string accessToken)
        {
        }

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="operation">anything (ignored)</param>
        public void SaveOperation(BankOperation operation)
        {
        }

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="operations">anything (ignored)</param>
        public void SaveOperations(List<BankOperation> operations)
        {
        }

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="account">anything (ignored)</param>
        public void SaveAccountBalance(IPublicAccount account)
        {
        }

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="account">anything (ignored)</param>
        /// <param name="operation">anything (ignored)</param>
        public void SaveOperation(IPublicAccount account, BankOperation operation)
        {
        }
    }
}