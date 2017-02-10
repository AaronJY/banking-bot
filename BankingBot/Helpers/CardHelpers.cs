using System;

namespace BankingBot.Helpers
{
    public static class CardHelpers
    {
        public static string[] SplitCardNumber(string cardNumber)
        {
            if (cardNumber.Length != 16)
                throw new ArgumentException("Card number was have a length of 16 characters.");

            return new[]
            {
                cardNumber.Substring(0, 4),
                cardNumber.Substring(4, 4),
                cardNumber.Substring(8, 4),
                cardNumber.Substring(12, 4)
            };
        }
    }
}
