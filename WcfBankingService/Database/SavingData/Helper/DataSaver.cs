using WcfBankingService.Accounts;
using WcfBankingService.operation;

namespace WcfBankingService.Database.SavingData.Helper
{
    public class DataSaver
    {
        public void SaveToken(int userId, string accessToken)
        {
            throw new System.NotImplementedException();
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