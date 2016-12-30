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

        public void SaveAccountBalance(IAccount account)
        {
            throw new System.NotImplementedException();
        }

        public void SaveOperationToHistory(int accountId, OperationRecord operationOperationRecord)
        {
            throw new System.NotImplementedException();
        }
    }
}