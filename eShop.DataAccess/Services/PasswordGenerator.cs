using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace eShop.DataAccess.Services
{
    public static class PasswordGenerator
    {
        private static int saltLimit = 32;
        public static byte[] GetSalt()
        {
            return GetSalt(saltLimit);
        }
        public static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        public static byte[] Hash(string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }

        public static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();

            return new SHA256Managed().ComputeHash(saltedValue);
        }
    }
}
