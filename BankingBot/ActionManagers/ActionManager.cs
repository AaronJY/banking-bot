using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBot.ActionManagers
{
    public abstract class ActionManager
    {
        protected IBrowserBot BrowserBot { get; private set; }

        public ActionManager(IBrowserBot browserBot)
        {
            BrowserBot = browserBot;
        }

        protected Type GetActionTypeFromInterface(object identifyingType, Type interfaceType)
        {
            var provider = ProviderIdentifier.GetProviderFromType(identifyingType.GetType());

            // Get all types implementing the given interface
            var typesImplementingInterface = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p =>
                    interfaceType.IsAssignableFrom(p) &&
                    p != interfaceType);

            foreach (var type in typesImplementingInterface)
            {
                var typeProvider = ProviderIdentifier.GetProviderFromType(type);
                if (typeProvider == provider)
                    return type;
            }

            return null;
        }
    }
}
