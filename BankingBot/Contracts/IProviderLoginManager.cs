namespace BankingBot.Contracts
{
    public interface IProviderLoginManager
    {
        void Login(ILoginCredentials credentials);
    }
}