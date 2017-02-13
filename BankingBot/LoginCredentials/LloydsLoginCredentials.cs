using BankingBot.Attributes;

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
