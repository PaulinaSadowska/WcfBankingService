using System;
using System.Linq;

namespace WcfBankingService.Users.Access
{
    /// <summary>
    /// <see cref="IPasswordComparator"/>
    /// </summary>
    public class PasswordComparator : IPasswordComparator
    {
        private readonly StandardPasswordHasher _hasher;

        public PasswordComparator()
        {
            _hasher = new StandardPasswordHasher();
        }

        /// <summary>
        /// <see cref="IPasswordComparator.ArePasswordsSame"/>
        /// </summary>
        public bool ArePasswordsSame(string hashedPassword, string password)
        {
            var bytes = Convert.FromBase64String(hashedPassword);
            var salt = bytes.Skip(StandardPasswordHasher.PasswordHashLength).ToArray();
            return _hasher.HashPassword(password, salt) == hashedPassword;
        }
    }
}