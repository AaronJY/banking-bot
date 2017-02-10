using BankingBot.Attributes;
using BankingBot.Enums;

namespace BankingBot.LoginCredentials
{
    [ProviderIdentifier(Provider.Barclays)]
    public class BarclaysLoginCredentials : LoginCredentials
    {
        public string Surname;
        public string MembershipNumber;
        public string CardNumber;
        public string SortCode;
        public string AccountNumber;
        public string Passcode;
        public string MemorableWord;
    }
}
