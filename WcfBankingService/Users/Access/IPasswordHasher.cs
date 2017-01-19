namespace WcfBankingService.Users.Access
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes password with given salt
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <param name="salt">salt</param>
        /// <returns>hashed password</returns>
        string HashPassword(string password, byte[] salt);

        /// <summary>
        /// Hashes password with random salt
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <returns>hashed password</returns>
        string HashPassword(string password);
    }
}
