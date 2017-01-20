using System;
using System.Security.Cryptography;

namespace WcfBankingService.Users.Access
{
    /// <summary>
    /// <see cref="IPasswordHasher"/>
    /// </summary>
    public class StandardPasswordHasher : IPasswordHasher
    {
        public const int PasswordHashLength = 20;
        public const int SaltLength = 16;
        private const int DeriveKeyIterrations = 10000;

        private readonly RNGCryptoServiceProvider _cryptoServiceProvider;

        public StandardPasswordHasher()
        {
            _cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// <see cref="IPasswordHasher.HashPassword"/>
        /// </summary>
        public string HashPassword(string password, byte[] salt)
        {
            if (salt == null)
                throw new ArgumentNullException(nameof(salt));

            if (salt.Length != SaltLength)
                throw new ArgumentException($"Salt is to short. Expected length: {SaltLength}");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("HashedPassword cannot be empty");

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, DeriveKeyIterrations);

            var hash = pbkdf2.GetBytes(PasswordHashLength);

            var saltedHash = new byte[PasswordHashLength + SaltLength];

            hash.CopyTo(saltedHash, 0);
            salt.CopyTo(saltedHash, PasswordHashLength);

            return Convert.ToBase64String(saltedHash);
        }

        /// <summary>
        /// <see cref="IPasswordHasher.HashPassword"/>
        /// </summary>
        public string HashPassword(string password)
        {
            var salt = new byte[SaltLength];
            _cryptoServiceProvider.GetBytes(salt);

            return HashPassword(password, salt);
        }
    }
}