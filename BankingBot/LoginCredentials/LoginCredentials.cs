using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;

namespace BankingBot.LoginCredentials
{
    public abstract class LoginCredentials : ILoginCredentials
    {
        public Provider? GetProvider()
        {
            return ProviderIdentifier.GetProviderFromType(GetType());
        }
    }
}
