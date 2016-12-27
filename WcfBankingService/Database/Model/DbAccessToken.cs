using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    [Table(Name = "AccessTokens")]
    public class DbAccessToken
    {
        [PrimaryKey]
        public int Id;
        [Column(Name = "user_id"), NotNull]
        public int UserId;
        [Column(Name = "accessToken"), NotNull]
        public string Token;

        public DbAccessToken()
        {

        }
    }
}