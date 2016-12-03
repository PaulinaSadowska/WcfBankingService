using System.Collections.Generic;
using WcfBankingService.account.number;

namespace WcfBankingService.User
{
    public interface IUser
    {
        string GenerateAccessToken(string password);

        IEnumerable<AccountNumber> GetAccountNumbers(string accessToken);

        string Login { get; }

        bool AddAccountNumber(string accessToken, AccountNumber accountNumber);
    }
}