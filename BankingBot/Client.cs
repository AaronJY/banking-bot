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
using BankingBot.Enums;

namespace BankingBot
{
    public class Client <T> : IClient
        where T : IWebDriver
    {
        #region Dependencies
        readonly ILoginManager loginManager;
        readonly IAccountManager accountManager;

        protected readonly IBrowserBot BrowserBot;
        #endregion

        public ILoginCredentials LoginCredentials { get; private set; }
        public Provider Provider { get; private set; }

        public bool IsLoggedIn
        {
            get { return LoginCredentials != null; }
        }

        public Client()
        {
            BrowserBot = new BrowserBot<T>();

            loginManager = new LoginManager(BrowserBot);
            accountManager = new AccountManager(BrowserBot);
        }

        #region Actions - Login Manager

        public Response Login(ILoginCredentials credentials)
        {
            LoginCredentials = credentials;
            Provider = credentials.GetProvider();

            var response = loginManager.Login(credentials);
            if (response.Status == ResponseStatus.Success)
            {
                accountManager.Init(Provider);
            }

            return response;
        }

        #endregion

        public decimal GetBalance()
        {
            throw new NotImplementedException();
        }

        #region Actions - Account Manager

        public IEnumerable<Account> GetAccounts()
        {
            return accountManager.GetAccounts();
        }

        #endregion
    }
}
