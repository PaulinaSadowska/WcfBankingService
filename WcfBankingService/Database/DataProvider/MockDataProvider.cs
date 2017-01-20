using System.Collections.Generic;
using WcfBankingService.Users;

namespace WcfBankingService.Database.DataProvider
{
    /// <summary>
    /// mocks data provider for test purposes
    /// </summary>
    public class MockDataProvider : IBankDataProvider
    {
        private List<IUser> Users { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="users">mock users list</param>
        public MockDataProvider(List<IUser> users)
        {
            Users = users;
        }

        /// <summary>
        /// <see cref="IBankDataProvider"/>
        /// </summary>
        /// <returns></returns>
        public List<IUser> GetStoredData()
        {
            return Users;
        }
    }
}