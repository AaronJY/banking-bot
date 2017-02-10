using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;
using BankingBot.LoginCredentials;
using BankingBot.Responses;
using OpenQA.Selenium;

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

                var cardSplit = Helpers.CardHelpers.SplitCardNumber(barcCreds.CardNumber);
                for (var i = 0; i < 4; i++)
                {
                    var fieldId = $"debitCardSet{i + 1}";
                    _browserBot.WebDriver.FindElement(By.Id(fieldId)).SendKeys(cardSplit[i]);
                }
            }

            _browserBot.WebDriver.FindElement(By.Id("forward")).Click();
        }
    }
}
