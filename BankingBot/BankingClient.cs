using System;
using System.Collections.Generic;
using BankingBot.Contracts;
using BankingBot.ActionManagers.LoginManagers;
using BankingBot.Models;
using OpenQA.Selenium;
using BankingBot.ActionManagers.AccountManagers;
using BankingBot.Responses;
using BankingBot.Enums;

namespace BankingBot
{
    public class BankingClient <T> : IClient, IDisposable
        where T : IWebDriver
    {
        #region Dependencies
        readonly ILoginManager loginManager;
        readonly IAccountManager accountManager;
        readonly IBrowserBot browserBot;
        #endregion

        public ILoginCredentials LoginCredentials { get; private set; }
        public Provider Provider { get; private set; }

        public bool IsLoggedIn
        {
            get { return LoginCredentials != null; }
        }

        public BankingClient()
        {
            browserBot = new BrowserBot<T>();
            loginManager = new LoginManager(browserBot);
            accountManager = new AccountManager(browserBot);
        }

        #region Actions - Login Manager

        public void Login(ILoginCredentials credentials)
        {
            LoginCredentials = credentials;
            Provider = credentials.GetProvider();

            loginManager.Login(credentials);
            accountManager.Init(Provider);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    browserBot.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BankingClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
