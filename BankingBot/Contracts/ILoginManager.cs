using BankingBot.Responses;

namespace BankingBot.Contracts
{
    public interface ILoginManager
    {
        void Login(ILoginCredentials credentials);
    }
}