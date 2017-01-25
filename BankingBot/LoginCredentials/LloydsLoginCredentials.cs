using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Attributes;
using BankingBot.Contracts;

namespace BankingBot.LoginCredentials
{
    [ProviderIdentifier(Enums.Provider.Lloyds)]
    public class LloydsLoginCredentials : LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Passphrase { get; set; }
    }
}
