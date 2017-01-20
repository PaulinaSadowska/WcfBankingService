using System.Collections.Generic;
using WcfBankingService.Users;

namespace WcfBankingService.Database.DataProvider
{
    /// <summary>
    /// Fetches user informations from the database (with accounts, access tokens and operation history)
    /// </summary>
    public interface IBankDataProvider
    {
        /// <summary>
        /// Restore all user informations from the database (with access tokens, history and accounts)
        /// </summary>
        /// <returns>list on users with all it's informations</returns>
        List<IUser> GetStoredData();
    }
}
