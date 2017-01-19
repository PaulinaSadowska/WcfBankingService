using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Access Token table data model
    /// </summary>
    [Table(Name = "AccessTokens")]
    public class DbAccessToken
    {
        [Column(Name = "user_id"), NotNull]
        public int UserId;
        [Column(Name = "accessToken"), NotNull]
        public string Token;
    }
}