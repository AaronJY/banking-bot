using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBot.Helpers
{
    public static class AccountHelpers
    {
        public static string[] SplitCardNumber(string cardNumber)
        {
            if (cardNumber.Length != 16)
                throw new ArgumentException("Card number must have a length of 16 characters.");

            return new[]
            {
                cardNumber.Substring(0, 4),
                cardNumber.Substring(4, 4),
                cardNumber.Substring(8, 4),
                cardNumber.Substring(12, 4)
            };
        }

        public static string[] SplitSortCode(string sortcode)
        {
            if (sortcode.Length != 6)
                throw new ArgumentException("Sortcode must have a length of 16 characters.");

            return new[]
            {
                sortcode.Substring(0, 2),
                sortcode.Substring(2, 2),
                sortcode.Substring(4, 2)
            };
        }
    }
}
