using System;
using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.LoginCredentials;
using OpenQA.Selenium;

namespace BankingBot.LoginManagers
{
    [ProviderIdentifier(Enums.Provider.Lloyds)]
    public class LloydsLoginManager : IProviderLoginManager
    {
        //private LloydsLoginCredentials _credentials;
        private readonly IBrowserBot _browserBot;
        
        public LloydsLoginManager(IBrowserBot browserBot)
        {
            _browserBot = browserBot;
        }

        public void Login(ILoginCredentials credentials)
        {
            var lloydsCreds = (LloydsLoginCredentials)credentials;

            _browserBot.WebDriver.Url = "https://online.lloydsbank.co.uk/personal/logon/login.jsp";
            _browserBot.WebDriver.Navigate();

            _browserBot.WebDriver.FindElement(By.Id("frmLogin:strCustomerLogin_userID")).SendKeys(lloydsCreds.Username);
            _browserBot.WebDriver.FindElement(By.Id("frmLogin:strCustomerLogin_pwd")).SendKeys(lloydsCreds.Password);

            _browserBot.WebDriver.FindElement(By.Id("frmLogin:btnLogin2")).Click();
        }
    }
}
