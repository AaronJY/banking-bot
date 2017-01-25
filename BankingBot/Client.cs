using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Contracts;
using BankingBot.ActionManagers.LoginManagers;
using BankingBot.Models;
using OpenQA.Selenium;
using BankingBot.ActionManagers.AccountManagers;
using BankingBot.Responses;

namespace BankingBot
{
    public class Client <T> : IClient
        where T : IWebDriver
    {
        #region Dependencies
        private readonly ILoginManager _loginManager;
        private readonly IAccountManager _accountManager;

        protected readonly IBrowserBot BrowserBot;
        #endregion

        public ILoginCredentials LoginCredentials { get; private set; }

        public bool IsLoggedIn
        {
            get { return LoginCredentials != null; }
        }

        public Client()
        {
            BrowserBot = new BrowserBot<T>();

            _loginManager = new LoginManager(BrowserBot);
            _accountManager = new AccountManager(BrowserBot);
        }

        #region Actions - Login Manager

        public Response Login(ILoginCredentials credentials)
        {
            LoginCredentials = credentials;

            return _loginManager.Login(credentials);
        }

        #endregion

        public decimal GetBalance()
        {
            throw new NotImplementedException();
        }

        #region Actions - Account Manager

        public IEnumerable<Account> GetAccounts()
        {
            return _accountManager.GetAccounts();
        }

        #endregion
    }
}
