using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;
using BankingBot.Models;
using BankingBot.Urls;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBot.ActionManagers.AccountManagers
{
    [ProviderIdentifier(Provider.Lloyds)]
    public class LloydsAccountManager : IProviderAccountManager
    {
        readonly IBrowserBot browserBot;
        readonly IScriptManager scriptManager;

        public LloydsAccountManager(
            IBrowserBot browserBot,
            IScriptManager scriptManager)
        {
            this.browserBot = browserBot;
            this.scriptManager = scriptManager;
        }

        public IEnumerable<Account> GetAccounts()
        {
            var accounts = new List<Account>();

            var accountsContainer = browserBot.WebDriver.FindElements(By.ClassName("des-m-sat-xx-account-information"));
            foreach (var container in accountsContainer)
            {
                var account = new Account();
                account.Name = container.FindElement(By.ClassName("account-name")).Text;
                account.AccountNumber = container.FindElement(By.ClassName("account-number")).Text;
                account.SortCode = container.FindElement(By.CssSelector("dd[aria-label='12 34 56']")).Text;

                string balanceTxt;
                decimal balance;

                // Split logic for getting Current Account AVAILABLE balance
                // or a normal account's balance
                try
                {
                    balanceTxt = container.FindElement(By.ClassName("available-balance")).Text;
                }
                catch (NoSuchElementException)
                {
                    balanceTxt = container.FindElement(By.ClassName("balance")).FindElement(By.TagName("span")).Text;
                }

                decimal.TryParse(balanceTxt, NumberStyles.Currency, new CultureInfo("en-GB"), out balance);
                account.Balance = balance;

                accounts.Add(account);
            }

            return accounts;
        }
    }
}
