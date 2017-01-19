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

        public ITable<DbUser> Users => GetTable<DbUser>();
        public ITable<DbAccount> Accounts => GetTable<DbAccount>();
        public ITable<DbAccessToken> AccessTokens => GetTable<DbAccessToken>();
        public ITable<DbOperationRecord> OperationRecord => GetTable<DbOperationRecord>();
    }
}