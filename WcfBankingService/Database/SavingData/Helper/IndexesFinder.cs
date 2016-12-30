using System.Linq;
using System.ServiceModel;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.Model;
using WcfBankingService.SoapService.DataContract.Response;

namespace WcfBankingService.Database.SavingData.Helper
{
    public class IndexesFinder
    {
        public int GetUserId(string login)
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Users
                            where p.Login == login
                            select p;
                var searchedUser = query.ToList()
                    .FirstOrDefault();
                if(searchedUser==null)
                    throw new FaultException("Internal Error");
                return searchedUser.Id;
            }
        }

        public int GetAccountId(string innerAccountNumber)
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Accounts
                            where p.InnerAccountNumber == innerAccountNumber
                            select p;
                var searchedAccount = query.ToList()
                    .FirstOrDefault();
                if (searchedAccount == null)
                    throw new FaultException("Internal Error");
                return searchedAccount.Id;
            }
        }
    }
}