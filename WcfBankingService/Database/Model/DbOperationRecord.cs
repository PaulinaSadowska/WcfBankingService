using LinqToDB.Mapping;
using WcfBankingService.operation;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Operation Record table data model
    /// </summary>
    [Table(Name = "OperationHistory")]
    public class DbOperationRecord
    {
        [Column(Name = "account_id"), NotNull] public int AccountId;

        [Column(Name = "source"), NotNull] public string Source;

        [Column(Name = "title"), NotNull] public string Title;

        [Column(Name = "dt"), NotNull] public decimal Debet;

        [Column(Name = "ct"), NotNull]
        public decimal Credit;

        [Column(Name = "balanceAfterOperation")] public decimal BalanceAfterOperation;

        public DbOperationRecord()
        {
            
        }

        public DbOperationRecord(int accountId, OperationRecord operationRecord)
        {
            AccountId = accountId;
            Debet = operationRecord.Debet;
            Credit = operationRecord.Credit;
            BalanceAfterOperation = operationRecord.BalanceAfterOperation;
            Source = operationRecord.Source;
            Title = operationRecord.Title;
        }
    }
}