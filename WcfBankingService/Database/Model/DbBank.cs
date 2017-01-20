using LinqToDB;
using LinqToDB.Data;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Bank database model with all it's tables
    /// </summary>
    public class DbBank : DataConnection
    {
        public DbBank() : base("bank") { }

        /// <summary>
        /// users table
        /// </summary>
        public ITable<DbUser> Users => GetTable<DbUser>();

        /// <summary>
        /// accounts table
        /// </summary>
        public ITable<DbAccount> Accounts => GetTable<DbAccount>();

        /// <summary>
        /// access tokens table
        /// </summary>
        public ITable<DbAccessToken> AccessTokens => GetTable<DbAccessToken>();

        /// <summary>
        /// operation records table
        /// </summary>
        public ITable<DbOperationRecord> OperationRecord => GetTable<DbOperationRecord>();
    }
}