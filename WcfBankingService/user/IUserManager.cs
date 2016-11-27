using System.Collections.Generic;
using WcfBankingService.accunt;

namespace WcfBankingService.user
{
    public interface IUserManager
    {
        // if sign in was successfull - return user token
        // if not - null. 
        // Only with token you are able to get user account numbers
        string signIn(string login, string password);
        IEnumerable<AccountNumber> getAccountNumbersFromUser(string userToken);
    }
}