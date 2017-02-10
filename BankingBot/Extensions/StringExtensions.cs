using System;
using System.Text.RegularExpressions;

namespace BankingBot.Extensions
{
    public static class StringExtensions
    {
        public static int AsInteger(this string str)
        {
            return int.Parse(Regex.Replace(str, @"[^\d]", ""));
        }
    }
}
