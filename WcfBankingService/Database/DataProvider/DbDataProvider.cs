using System;
using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Database.Model;
using WcfBankingService.Users;

namespace WcfBankingService.Database.DataProvider
{
    public class DbDataProvider : IBankDataProvider
    {
        public List<IUser> GetStoredData()
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Users
                            select p;
                var dbUsers = query.ToList();
                var users = new List<IUser>();
                foreach (var user in dbUsers)
                {
                    users.Add(new User(user.login, user.password));
                }
                return users;
            }
        }
    }
}