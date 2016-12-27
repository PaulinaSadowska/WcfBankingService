using LinqToDB;
using LinqToDB.Data;
using WcfBankingService.Users;

namespace WcfBankingService.Database.Model
{
    public class DbBank : DataConnection
    {
        public DbBank() : base("bank") { }

        public ITable<DbAccessToken> AccessTokens => GetTable<DbAccessToken>();
        public ITable<User> Users => GetTable<User>();
    }
}