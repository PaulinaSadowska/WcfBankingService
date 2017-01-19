using System.Linq;
using System.ServiceModel;
using WcfBankingService.Database.Model;

namespace WcfBankingService.Database.SavingData.Helper
{
    public class IndexesFinder
    {
        /// <summary>
        /// Finds user id in database for a given login
        /// </summary>
        /// <param name="login">user login</param>
        /// <returns>user id</returns>
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

        /// <summary>
        /// Finds account id for a given inner account number
        /// </summary>
        /// <param name="innerAccountNumber">inner account number (without bank id and control sum)</param>
        /// <returns>account id</returns>
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