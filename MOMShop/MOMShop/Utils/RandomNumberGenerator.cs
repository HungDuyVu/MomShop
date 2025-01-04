using System.Text;
using System;

namespace MOMShop.Utils
{
    public class RandomNumberGenerator
    {
        private static Random random = new Random();

        public static string GenerateRandomNumber(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int randomNumber = random.Next(0, 10); // Sinh số ngẫu nhiên từ 0 đến 9
                sb.Append(randomNumber);
            }
            return sb.ToString();
        }
    }
}
