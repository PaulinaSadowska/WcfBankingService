using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToDB.Mapping;

namespace WcfBankingService.Database
{
    [Table(Name = "AccessTokens")]
    public class AccessToken
    {
        [PrimaryKey]
        public int Id;
        [Column(Name = "user_id"), NotNull]
        public int UserId;
        [Column(Name = "accessToken"), NotNull]
        public string Token;

        public AccessToken()
        {

        }
    }
}