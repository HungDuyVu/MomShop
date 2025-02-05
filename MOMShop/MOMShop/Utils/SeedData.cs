﻿using MOMShop.Entites;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MOMShop.Utils
{
    public static class SeedData
    {
        private static Random random = new Random();
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

        public static string RandomPassword()
        {
            const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$^*()";
            Random rng = new Random();
            return new string(Enumerable.Repeat(AllowedChars, 8)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
