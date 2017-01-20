using System.Linq;
using LinqToDB;
using WcfBankingService.Accounts;
using WcfBankingService.Database.Model;
using WcfBankingService.operation;

namespace WcfBankingService.Database.SavingData.Helper
{
    /// <summary>
    /// halper class used to save data into database
    /// </summary>
    public class DataSaver
    {
        /// <summary>
        /// Inserts Access Token into database
        /// </summary>
        /// <param name="userId">user id (stored in database)</param>
        /// <param name="accessToken">access token to save</param>
        public void SaveToken(int userId, string accessToken)
        {
            var token = new DbAccessToken
            {
                UserId = userId,
                Token = accessToken
            };
            using (var db = new DbBank())
            {
                db.Insert(token);
            }
        }

        /// <summary>
        /// Save account balance into database
        /// </summary>
        /// <param name="account">account which balance will be saved</param>
        public void SaveAccountBalance(IPublicAccount account)
        {
            using (var db = new DbBank())
            {
                db.Accounts
                    .Where(p => p.InnerAccountNumber == account.AccountNumber.InnerNumber)
                    .Set(p=> p.BalanceValue, account.GetBalanceValue())
                    .Update();
            }
        }

        /// <summary>
        /// Save in operation history of given account the operation record
        /// </summary>
        /// <param name="accountId">account id (from database)</param>
        /// <param name="operationRecord">operation record to save</param>
        public void SaveOperationToHistory(int accountId, OperationRecord operationRecord)
        {
            using (var db = new DbBank())
            {
                db.Insert(new DbOperationRecord(accountId, operationRecord));

            }
        }
    }
}