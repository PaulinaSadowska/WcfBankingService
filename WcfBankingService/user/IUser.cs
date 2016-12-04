using System.Collections.Generic;
using WcfBankingService.account;
using WcfBankingService.account.number;

namespace WcfBankingService.User
{
    public interface IUser
    {
        string Login { get; }

        string GenerateAccessToken(string password);

        IEnumerable<Account> GetAllAccounts(string accessToken);

        Account GetAccount(string accessToken, AccountNumber accountNumber);

        bool AddAccount(string accessToken, Account account);
    }
}