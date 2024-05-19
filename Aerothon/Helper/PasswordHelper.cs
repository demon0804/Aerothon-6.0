using Aerothon.Helper.Interfaces;
using System.Security.Cryptography;

namespace Aerothon.Helper
{
    public class PasswordHelper : IPasswordHelper
    {
        // Constants
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>
        /// Hash password
        /// </returns>
        public string HashPassword(string password)
        {
            // Generate a random salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(KeySize);

                    byte[] hashBytes = new byte[SaltSize + KeySize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns>true/ false</returns>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);

                for (int i = 0; i < KeySize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

