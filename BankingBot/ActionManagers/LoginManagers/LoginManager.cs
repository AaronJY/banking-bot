using System;
using System.Linq;
using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Responses;

namespace BankingBot.ActionManagers.LoginManagers
{
    public class LoginManager : ActionManager, ILoginManager
    {
        public LoginManager(IBrowserBot browserBot)
            : base(browserBot)
        { }

        public Response Login(ILoginCredentials credentials)
        {
            var provLoginManagerType = GetActionTypeFromInterface(credentials, typeof(IProviderLoginManager));
            var provLoginManager = (IProviderLoginManager)Activator.CreateInstance(provLoginManagerType, BrowserBot);

            return provLoginManager.Login(credentials);
        }
    }
}
