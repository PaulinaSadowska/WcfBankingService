using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Access Token table data model
    /// </summary>
    [Table(Name = "AccessTokens")]
    public class DbAccessToken
    {
        /// <summary>
        /// user id
        /// </summary>
        [Column(Name = "user_id"), NotNull]
        public int UserId;

        /// <summary>
        /// token used to perform operations on the user's account
        /// </summary>
        [Column(Name = "accessToken"), NotNull]
        public string Token;
    }
}