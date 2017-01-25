using BankingBot.Enums;

namespace BankingBot.Contracts
{
    public interface ILoginCredentials
    {
        Provider? GetProvider();
    }
}