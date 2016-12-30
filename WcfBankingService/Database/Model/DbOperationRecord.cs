using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    [Table(Name = "OperationHistory")]
    public class DbOperationRecord
    {
        [Column(Name = "account_id"), NotNull] public int AccountId;

        [Column(Name = "source"), NotNull] public string Source;

        [Column(Name = "title"), NotNull] public string Title;

        [Column(Name = "amount"), NotNull] public decimal Amount;

        [Column(Name = "balanceAfterOperation")] public decimal BalanceAfterOperation;
    }
}