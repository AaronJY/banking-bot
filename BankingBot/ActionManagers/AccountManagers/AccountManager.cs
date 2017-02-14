using BankingBot.Contracts;
using System;
using System.Collections.Generic;
using BankingBot.Models;
using BankingBot.Enums;
using BankingBot.ScriptManagement;

namespace BankingBot.ActionManagers.AccountManagers
{
    public class AccountManager : ActionManager, IAccountManager
    {
        protected IProviderAccountManager providerAccountManager;
        private Provider _provider;

        public AccountManager(IBrowserBot browserBot)
            : base(browserBot)
        { }

        public void Init(Provider provider)
        {
            _provider = provider;

            // TODO: Implement DI to get rid of this new() crap
            var scriptManager = new ScriptManager(BrowserBot);

            var providerAccountManagerType = GetTypeFromInterface(provider, typeof(IProviderAccountManager));
            providerAccountManager = (IProviderAccountManager)Activator.CreateInstance(providerAccountManagerType, BrowserBot, scriptManager);
        }

        #region Behaviours

        public IEnumerable<Account> GetAccounts()
        {
            return providerAccountManager.GetAccounts();
        }

        public IEnumerable<Transaction> GetTransactions(string accountNumber)
        {
            return providerAccountManager.GetTransactions(accountNumber);
        }

        #endregion
    }
}
