using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Utils.Hash
{
    public static class PasswordTool
    {
        public static string HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            // derive a 256-bit subkey (use HMACSHA1 with 5000 iterations)
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                5000,
                256 / 8)
            );
            return $"{hash}.{Convert.ToBase64String(salt)}";
        }

        public static bool ValidatePassword(string password, string hashedString)
        {
            var authElements = hashedString.Split('.');
            var hashedPassword = authElements[0];
            var salt = Convert.FromBase64String(authElements[1]);
            // derive a 256-bit subkey (use HMACSHA1 with 5000 iterations)
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                5000,
                256 / 8)
            );
            return hash.Equals(hashedPassword);
        }
    }
}
