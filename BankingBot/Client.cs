using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Contracts;
using BankingBot.LoginManagers;
using BankingBot.Models;
using OpenQA.Selenium;

namespace BankingBot
{
    public class Client <T> : IClient
        where T : IWebDriver
    {
        #region Dependencies
        private readonly ILoginManager _loginManager;
        protected readonly IBrowserBot BrowserBot;
        #endregion

        public ILoginCredentials LoginCredentials { get; private set; }

        public Client()
        {
            BrowserBot = new BrowserBot<T>();
            _loginManager = new LoginManager(BrowserBot);
        }

        #region Actions

        public void Login(ILoginCredentials credentials)
        {
            LoginCredentials = credentials;

            _loginManager.Login(credentials);
        }

        public decimal GetBalance()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
