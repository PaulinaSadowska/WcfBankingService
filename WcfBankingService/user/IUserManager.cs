using System.Collections.Generic;
using WcfBankingService.account.number;

namespace WcfBankingService.user
{
    public interface IUserManager
    {
        // if sign in was successfull - return user token
        // if not - null. 
        // Only with token you are able to get user account numbers
        string SignIn(string login, string password);
        string SignUp(string login, string password);
        void AddAccountNumber(string userToken, AccountNumber account);
        IEnumerable<AccountNumber> GetAccountNumbersFromUser(string userToken);


    }
}