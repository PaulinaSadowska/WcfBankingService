
using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Users table data model
    /// </summary>
    [Table(Name = "Users")]
    public class DbUser
    {
        /// <summary>
        /// user id
        /// </summary>
        [PrimaryKey] public int Id;

        /// <summary>
        /// user login
        /// </summary>
        [Column(Name = "login"), NotNull] public string Login;

        /// <summary>
        /// user password hashed with IPasswordHasher
        /// </summary>
        [Column(Name = "password"), NotNull] public string HashedPassword;
    }
}