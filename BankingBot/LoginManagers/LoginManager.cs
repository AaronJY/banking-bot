using System;
using System.Linq;
using BankingBot.Attributes;
using BankingBot.Contracts;
using OpenQA.Selenium;

namespace BankingBot.LoginManagers
{
    public class LoginManager : ILoginManager
    {
        private readonly IBrowserBot _browserBot;

        public LoginManager(IBrowserBot browserBot)
        {
            _browserBot = browserBot;
        }

        public void Login(ILoginCredentials credentials)
        {
            var provLoginManagerType = GetProviderLoginManagerType(credentials);
            var provLoginManager = (IProviderLoginManager)Activator.CreateInstance(provLoginManagerType, _browserBot);
            provLoginManager.Login(credentials);
        }

        private Type GetProviderLoginManagerType(ILoginCredentials credentials)
        {
            // Get all "provider login manager" classes in the assembly
            // (any class that implements IProviderLoginManager)
            var providerLoginManagerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p =>
                    typeof(IProviderLoginManager).IsAssignableFrom(p) &&
                    p != typeof(IProviderLoginManager));

            var credentialsProvider = credentials.GetProvider();
            foreach (var type in providerLoginManagerTypes)
            {
                // Get associated provider for each "provider login manager" class
                var provider = ProviderIdentifier.GetProviderFromType(type);
                if (provider == credentialsProvider)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
