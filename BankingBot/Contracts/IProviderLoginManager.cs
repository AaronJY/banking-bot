using BankingBot.Responses;

namespace BankingBot.Contracts
{
    public interface IProviderLoginManager
    {
        Response Login(ILoginCredentials credentials);
    }
}