using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingBot.ActionManagers
{
    public abstract class ActionManager
    {
        protected IBrowserBot BrowserBot { get; private set; }

        public ActionManager(IBrowserBot browserBot)
        {
            BrowserBot = browserBot;
        }

        protected ActionDetail GetActionDetailFromInterface(object identifyingType, Type interfaceType)
        {
            var provider = ProviderIdentifier.GetProviderFromType(identifyingType.GetType());
            var type = GetTypeAssociatedWithProvider(provider, interfaceType);

            return new ActionDetail { Provider = provider, Type = type };
        }

        protected Type GetTypeFromInterface(Provider provider, Type interfaceType)
        {
            return GetTypeAssociatedWithProvider(provider, interfaceType);
        }

        private static IEnumerable<Type> GetTypesImplementingInterface(Type interfaceType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p =>
                    interfaceType.IsAssignableFrom(p) &&
                    p != interfaceType).ToList();
        }

        private static Type GetTypeAssociatedWithProvider(Provider provider, Type interfaceType)
        {
            var typesImplementingInterface = GetTypesImplementingInterface(interfaceType);
            foreach (var type in typesImplementingInterface)
            {
                var typeProvider = ProviderIdentifier.GetProviderFromType(type);
                if (typeProvider == provider)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
