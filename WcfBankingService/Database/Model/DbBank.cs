using LinqToDB;
using LinqToDB.Data;

namespace WcfBankingService.Database.Model
{
    public class DbBank : DataConnection
    {
        public DbBank() : base("bank") { }

        public ITable<DbAccessToken> AccessTokens => GetTable<DbAccessToken>();
        public ITable<DbUser> Users => GetTable<DbUser>();
    }
}