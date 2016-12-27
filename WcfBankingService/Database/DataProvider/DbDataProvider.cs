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
            var users = GetUsersFromDb();
            return users;
        }

        private static List<IUser> GetUsersFromDb()
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Users
                            select p;
                return query.ToList().Select(user => new User(user.login, user.password))
                    .Cast<IUser>().ToList();
            }
        }
    }
}