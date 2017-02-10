using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;
using BankingBot.LoginCredentials;
using BankingBot.Responses;
using BankingBot.Helpers;
using OpenQA.Selenium;
using System;

namespace BankingBot.ActionManagers.LoginManagers
{
    [ProviderIdentifier(Provider.Barclays)]
    public class BarclaysLoginManager : IProviderLoginManager
    {
        private readonly IScriptManager _scriptManager;
        private readonly IBrowserBot _browserBot;

        private static class Urls
        {
            public const string Login = "https://bank.barclays.co.uk/olb/auth/LoginLink.action";
        }

        public BarclaysLoginManager(
            IBrowserBot browserBot,
            IScriptManager scriptManager)
        {
            _browserBot = browserBot;
            _scriptManager = scriptManager;
        }

        public Response Login(ILoginCredentials credentials)
        {
            var barcCreds = credentials as BarclaysLoginCredentials;

            _browserBot.WebDriver.Url = Urls.Login;
            _browserBot.WebDriver.Navigate();

            _browserBot.WebDriver.FindElement(By.Id("surname")).SendKeys(barcCreds.Surname);

            // Chosen to use membership number
            if (barcCreds.MembershipNumber != null)
            {
                _browserBot.WebDriver.FindElement(By.Id("membership-radio")).Click();
                _browserBot.WebDriver.FindElement(By.Id("membership-num")).SendKeys(barcCreds.MembershipNumber);
            }
            // Chosen to use card number
            else if (barcCreds.CardNumber != null)
            {
                _browserBot.WebDriver.FindElement(By.Id("card-radio")).Click();

                var cardSplit = AccountHelpers.SplitCardNumber(barcCreds.CardNumber);
                for (var i = 0; i < 4; i++)
                {
                    var fieldId = $"debitCardSet{i + 1}";
                    _browserBot.WebDriver.FindElement(By.Id(fieldId)).SendKeys(cardSplit[i]);
                }
            }
            // Chosen to use account details
            else if (barcCreds.SortCode != null || barcCreds.AccountNumber != null)
            {
                _browserBot.WebDriver.FindElement(By.Id("account-radio")).Click();

                var sortcodeSplit = AccountHelpers.SplitSortCode(barcCreds.SortCode);
                for (var i = 0; i < 3; i++)
                {
                    var fieldId = $"sortCodeSet{i + 1}";
                    _browserBot.WebDriver.FindElement(By.Id(fieldId)).SendKeys(sortcodeSplit[i]);
                }
            }
            else
            {
                throw new InvalidOperationException("Could not determine login procedure from given properties.");
            }

            // Advance to stage 2
            _browserBot.WebDriver.FindElement(By.Id("forward")).Click();

            _browserBot.WebDriver.FindElement(By.Id("passcode-radio")).Click();
            _browserBot.WebDriver.FindElement(By.Id("passcode")).SendKeys(barcCreds.Passcode);

            var characters = _browserBot.WebDriver.FindElement(By.ClassName("letter-select"))
                .FindElements(By.TagName("strong"));

        }
    }
}
