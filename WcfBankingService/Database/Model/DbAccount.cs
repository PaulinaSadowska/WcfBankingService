using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Accounts table data model
    /// </summary>
    [Table(Name = "Accounts")]
    public class DbAccount
    {
        /// <summary>
        /// account id
        /// </summary>
        [PrimaryKey] public int Id;

        /// <summary>
        /// user id
        /// </summary>
        [Column(Name = "user_id"), NotNull] public int UserId;

        /// <summary>
        /// balance value
        /// </summary>
        [Column(Name = "balance"), NotNull] public decimal BalanceValue;

        /// <summary>
        /// client's account number (banks inner account number, without control sum and bankid)
        /// </summary>
        [Column(Name = "innerAccountNum"), NotNull] public string InnerAccountNumber;
    }
}