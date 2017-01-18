namespace WcfBankingService.Users.Access
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, byte[] salt);
        string HashPassword(string password);
    }
}
