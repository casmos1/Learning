using System;
using System.Security.Cryptography;
using System.Text;

namespace Yoda
{
    public static class Security
    {
        public static string CreateSalt()
        {
            const int saltSize = 20;
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[saltSize];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateShawHash(string input, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha512Managed = new SHA512Managed();
            var hash = sha512Managed.ComputeHash(bytes);

            return Encoding.Default.GetString(hash);
        } 
    }
}