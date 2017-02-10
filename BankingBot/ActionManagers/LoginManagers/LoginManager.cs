using System;
using System.Linq;
using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Responses;
using BankingBot.ScriptManagement;

namespace BankingBot.ActionManagers.LoginManagers
{
    public class LoginManager : ActionManager, ILoginManager
    {
        public LoginManager(IBrowserBot browserBot)
            : base(browserBot)
        { }

        public Response Login(ILoginCredentials credentials)
        {
            // TODO: THIS NEEDS TO BE MOVED
            var scriptManager = new ScriptManager(BrowserBot);

            var provLoginManagerType = GetActionTypeFromInterface(credentials, typeof(IProviderLoginManager));
            var provLoginManager = (IProviderLoginManager)Activator.CreateInstance(provLoginManagerType, BrowserBot, scriptManager);

            return provLoginManager.Login(credentials);
        }
    }
}
