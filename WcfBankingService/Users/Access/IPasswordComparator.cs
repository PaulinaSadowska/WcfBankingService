namespace WcfBankingService.Users.Access
{
    public interface IPasswordComparator
    {
        /// <summary>
        /// Checks if not hashed password is (after hashing) the same as hashed password
        /// </summary>
        /// <param name="hashedPassword">hashed password</param>
        /// <param name="password">not hashed password</param>
        /// <returns>if not hashed password is (after hashing) the same as hashed password</returns>
        bool ArePasswordsSame(string hashedPassword, string password);
    }
}
