namespace BankingBot.Contracts
{
    public interface IClient
    {
        void Login(ILoginCredentials credentials);
    }
}