using OpenQA.Selenium;
using System;

namespace BankingBot.Contracts
{
    public interface IBrowserBot : IDisposable
    {
        IWebDriver WebDriver { get; }
    }
}