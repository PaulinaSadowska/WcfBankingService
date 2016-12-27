using System.Collections.Generic;
using WcfBankingService.Users;

namespace WcfBankingService.Database.DataProvider
{
    public interface IBankDataProvider
    {
        List<IUser> GetStoredData();
    }
}
