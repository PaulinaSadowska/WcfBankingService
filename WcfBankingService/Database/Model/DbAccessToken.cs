using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    [Table(Name = "AccessTokens")]
    public class DbAccessToken
    {
        [Column(Name = "user_id"), NotNull]
        public int UserId;
        [Column(Name = "accessToken"), NotNull]
        public string Token;
    }
}