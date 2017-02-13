using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;

namespace BankingBot.LoginCredentials
{
    public abstract class LoginCredentials : ILoginCredentials
    {
        public Provider GetProvider()
        {
            return ProviderIdentifier.GetProviderFromType(GetType());
        }
    }
}
