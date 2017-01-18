namespace WcfBankingService.Users.Access
{
    public interface IPasswordComparator
    {
        bool ArePasswordsSame(string hashedPassword, string password);
    }
}
