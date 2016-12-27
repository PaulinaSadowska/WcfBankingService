using LinqToDB;
using LinqToDB.Data;

namespace WcfBankingService.Database
{
    public class DbBank : DataConnection
    {
        public DbBank() : base("bank") { }

        public ITable<AccessToken> AccessTokens => GetTable<AccessToken>();

        // ... other tables ...
    }
}