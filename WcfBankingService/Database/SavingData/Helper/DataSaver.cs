using System.Linq;
using LinqToDB;
using WcfBankingService.Accounts;
using WcfBankingService.Database.Model;
using WcfBankingService.operation;

namespace WcfBankingService.Database.SavingData.Helper
{
    public class DataSaver
    {
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

        public void SaveOperationToHistory(int accountId, OperationRecord operationRecord)
        {
            using (var db = new DbBank())
            {
                db.Insert(new DbOperationRecord(accountId, operationRecord));

            }
        }
    }
}