using BankingBot.Responses;

namespace BankingBot.Contracts
{
    public interface ILoginManager
    {
        Response Login(ILoginCredentials credentials);
    }
}