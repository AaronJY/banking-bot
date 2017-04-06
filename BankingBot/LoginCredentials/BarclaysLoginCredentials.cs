using BankingBot.Attributes;
using BankingBot.Enums;

namespace BankingBot.LoginCredentials
{
    [ProviderIdentifier(Provider.Barclays)]
    public class BarclaysLoginCredentials : LoginCredentials
    {
        public string Surname { get; set; }

        public string MembershipNumber { get; set; }

        public string CardNumber { get; set; }

        public string SortCode { get; set; }

        public string AccountNumber { get; set; }

        public string Passcode { get; set; }

        public string MemorableWord { get; set; }
    }
}
