using System;

namespace Raptor.Core.Security
{
    public static class HashGenerator
    {
        public static string GenerateHash(string input, string salt)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            var hashAlgoritm = System.Security.Cryptography.MD5.Create();
            bytes = hashAlgoritm.ComputeHash(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static string CreateSalt()
        {
            var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var buff = new byte[25];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}