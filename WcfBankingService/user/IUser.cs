using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.User
{
    public interface IUser
    {
        string Login { get; }

        string GenerateAccessToken(string password);

        IEnumerable<IAccount> GetAllAccounts(string accessToken);

        IAccount GetAccount(string accessToken, AccountNumber accountNumber);

        bool AddAccount(string accessToken, IAccount account);
    }
}