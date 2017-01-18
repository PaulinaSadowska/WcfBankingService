﻿using System;
using System.Security.Cryptography;

namespace WcfBankingService.Users.Access
{
    public class StandardPasswordHasher : IPasswordHasher
    {
        public const int PasswordHashLength = 20;
        public const int SaltLength = 16;
        private const int DeriveKeyIterrations = 10000;

        private readonly RNGCryptoServiceProvider _cryptoServiceProvider = new RNGCryptoServiceProvider();

        private byte[] _hash;

        private byte[] _salt;

        private byte[] _saltedHash;

        public string HashPassword(string password, byte[] salt)
        {
            if (salt == null)
                throw new ArgumentNullException(nameof(salt));

            if (salt.Length != SaltLength)
                throw new ArgumentException($"Salt is to short. Expected length: {SaltLength}");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            _salt = salt;

            //Password-Based Key Derivation Function 2

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, DeriveKeyIterrations);

            _hash = pbkdf2.GetBytes(PasswordHashLength);

            _saltedHash = new byte[PasswordHashLength + SaltLength];

            _hash.CopyTo(_saltedHash, 0);
            salt.CopyTo(_saltedHash, PasswordHashLength);

            return Convert.ToBase64String(_saltedHash);
        }

        public string HashPassword(string password)
        {
            _salt = new byte[SaltLength];
            _cryptoServiceProvider.GetBytes(_salt);

            return HashPassword(password, _salt);
        }
    }
}