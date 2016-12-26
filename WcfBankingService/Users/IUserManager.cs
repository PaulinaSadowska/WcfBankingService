using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Users
{
    public interface IUserManager
    {
        string SignIn(string login, string password);

        bool SignUp(string login, string password);

        bool ContainsUser(string login);

        bool AddAccount(string login, string accessToken, IAccount account);

        IEnumerable<IAccount> GetAllAccounts(string login, string accessToken);

        IAccount GetAccount(string accessToken, AccountNumber accoutNumber);

    }
}