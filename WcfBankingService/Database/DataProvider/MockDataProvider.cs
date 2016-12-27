using System.Collections.Generic;
using WcfBankingService.Users;

namespace WcfBankingService.Database.DataProvider
{
    public class MockDataProvider : IBankDataProvider
    {
        private List<IUser> Users { get; }

        public MockDataProvider(List<IUser> users)
        {
            Users = users;
        }

        public List<IUser> GetStoredData()
        {
            return Users;
        }
    }
}