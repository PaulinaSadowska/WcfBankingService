using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Users
{
    public interface IUser
    {
        string Login { get; }

        string GenerateAccessToken(string password);

        ICollection<IAccount> GetAllAccounts(string accessToken);

        ICollection<string> GetAllAccountNumbers(string accessToken);

        IAccount GetAccount(string accessToken, AccountNumber accountNumber);

        IPublicAccount GetAccount(AccountNumber accountNumber);

        bool ContainsAccount(AccountNumber accountNumber);

        bool AddAccount(string accessToken, IAccount account);
    }
}