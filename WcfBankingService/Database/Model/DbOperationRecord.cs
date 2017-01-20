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
        /// <summary>
        /// account id
        /// </summary>
        [Column(Name = "account_id"), NotNull] public int AccountId;

        /// <summary>
        /// source of the money
        /// </summary>
        [Column(Name = "source"), NotNull] public string Source;

        /// <summary>
        /// title of the operation
        /// </summary>
        [Column(Name = "title"), NotNull] public string Title;

        /// <summary>
        /// debet value
        /// </summary>
        [Column(Name = "dt"), NotNull] public decimal Debet;

        /// <summary>
        /// credit value
        /// </summary>
        [Column(Name = "ct"), NotNull]
        public decimal Credit;

        /// <summary>
        /// balance on the account after operation
        /// </summary>
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