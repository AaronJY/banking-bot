using OpenQA.Selenium;

namespace BankingBot.Contracts
{
    public interface IBrowserBot
    {
        IWebDriver WebDriver { get; }
    }
}