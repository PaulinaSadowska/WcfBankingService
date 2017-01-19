
using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Users table data model
    /// </summary>
    [Table(Name = "Users")]
    public class DbUser
    {
        [PrimaryKey] public int Id;
        [Column(Name = "login"), NotNull] public string Login;
        [Column(Name = "password"), NotNull] public string HashedPassword;
    }
}