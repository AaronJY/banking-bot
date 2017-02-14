using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;
using BankingBot.Extensions;
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

        private enum TransactionTableColumn
        {
            Date = 0,
            Description = 1,
            Type = 2,
            In = 3,
            Out = 4,
            Balance = 5
        }

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

        public IEnumerable<Transaction> GetTransactions(string accountNumber)
        {
            var transactions = new List<Transaction>();

            var ajaxIdentifier = GetAjaxIdentifierForAccountNumber(accountNumber);
            var accountDetailsUrl = $"{LloydsUrls.AccountDetails}/{ajaxIdentifier}";
            browserBot.WebDriver.Url = accountDetailsUrl;

            var transactionsTableBody = browserBot.WebDriver
                .FindElement(By.ClassName("statements-wrap"))
                .FindElement(By.ClassName("cwa-tbody"));

            var transactionRows = transactionsTableBody.FindElements(By.CssSelector("tr[aria-expanded='false']"));
            foreach (var row in transactionRows)
            {
                var rowCells = row.FindElements(By.TagName("td"));

                var transaction = new Transaction
                {
                    Date = DateTime.Parse(rowCells[(int)TransactionTableColumn.Date].Text),
                    IsPending = false,
                    Description = rowCells[(int)TransactionTableColumn.Description].Text
                };

                var amountIn = rowCells[(int)TransactionTableColumn.In].Text;
                var amountOut = rowCells[(int)TransactionTableColumn.Out].Text;
                if (amountIn != "")
                {
                    transaction.Amount = decimal.Parse(amountIn);
                }
                else
                {
                    transaction.Amount = -(decimal.Parse(amountOut));
                }

                transactions.Add(transaction);
            }

            return transactions;
        }

        private string GetAjaxIdentifierForAccountNumber(string accountNumber)
        {
            var currentUrl = browserBot.WebDriver.Url;

            browserBot.WebDriver.Url = LloydsUrls.AccountOverview;

            IWebElement accountContainer = null;
            var accountContainers = browserBot.WebDriver.FindElements(By.ClassName("des-m-sat-xx-account-tile"));
            foreach (var cnt in accountContainers)
            {
                if (cnt.HasElement(By.ClassName("account-number")))
                {
                    accountContainer = cnt;
                    break;
                }
            }

            if (accountContainer == null)
                throw new InvalidOperationException($"Could not find account with account number '{accountNumber}'");

            return accountContainer.GetAttribute("data-ajax-identifier");
        }

    }
}
