﻿using System;
using System.Text;

namespace WcfBankingService.Users.Access
{
    public class AccessTokenGenerator
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        public static string Generate(int size)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var randomCharacter = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(randomCharacter);
            }
            return builder.ToString();
        }
    }
}