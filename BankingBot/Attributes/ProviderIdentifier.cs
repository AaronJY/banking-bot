using System;
using BankingBot.Enums;

namespace BankingBot.Attributes
{
    internal class ProviderIdentifier : Attribute
    {
        public readonly Provider Provider;

        public ProviderIdentifier(Provider provider)
        {
            Provider = provider;
        }

        public static Provider? GetProviderFromType(Type t)
        {
            foreach (var attr in t.GetCustomAttributes(false))
            {
                if ((ProviderIdentifier)attr != null)
                {
                    return ((ProviderIdentifier)attr).Provider;
                }
            }

            return null;
        }
    }
}
