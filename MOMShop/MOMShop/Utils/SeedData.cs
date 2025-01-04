using MOMShop.Entites;
using System.Security.Cryptography;
using System.Text;

namespace MOMShop.Utils
{
    public static class SeedData
    {
        public static Users Admin()
        {
            return new Users()
            {
                Email = "admin",
                Password = "46f94c8de14fb36680850768ff1b7f2a",
                UserType = 1
            };
        }
        public static string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
