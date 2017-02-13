using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.Enums;
using BankingBot.LoginCredentials;
using BankingBot.Responses;
using System.Collections.Generic;
using BankingBot.ScriptManagement;

namespace BankingBot.ActionManagers.LoginManagers
{
    [ProviderIdentifier(Provider.Barclays)]
    public class BarclaysLoginManager : IProviderLoginManager
    {
        readonly IScriptManager scriptManager;
        readonly IBrowserBot browserBot;
        private BarclaysLoginCredentials _credentials;

        private static class Urls
        {
            public const string Login = "https://bank.barclays.co.uk/olb/auth/LoginLink.action";
        }

        public BarclaysLoginManager(
            IBrowserBot browserBot,
            IScriptManager scriptManager)
        {
            this.browserBot = browserBot;
            this.scriptManager = scriptManager;
        }

        public void Login(ILoginCredentials credentials)
        {
            _credentials = credentials as BarclaysLoginCredentials;

            browserBot.WebDriver.Url = Urls.Login;
            browserBot.WebDriver.Navigate();

            //_browserBot.WebDriver.FindElement(By.Id("surname")).SendKeys(_credentials.Surname);

            //// Chosen to use membership number
            //if (_credentials.MembershipNumber != null)
            //{
            //    _browserBot.WebDriver.FindElement(By.Id("membership-radio")).Click();
            //    _browserBot.WebDriver.FindElement(By.Id("membership-num")).SendKeys(_credentials.MembershipNumber);
            //}
            //// Chosen to use card number
            //else if (_credentials.CardNumber != null)
            //{
            //    _browserBot.WebDriver.FindElement(By.Id("card-radio")).Click();

            //    var cardSplit = AccountHelpers.SplitCardNumber(_credentials.CardNumber);
            //    for (var i = 0; i < 4; i++)
            //    {
            //        var fieldId = $"debitCardSet{i + 1}";
            //        _browserBot.WebDriver.FindElement(By.Id(fieldId)).SendKeys(cardSplit[i]);
            //    }
            //}
            //// Chosen to use account details
            //else if (_credentials.SortCode != null || _credentials.AccountNumber != null)
            //{
            //    _browserBot.WebDriver.FindElement(By.Id("account-radio")).Click();

            //    var sortcodeSplit = AccountHelpers.SplitSortCode(_credentials.SortCode);
            //    for (var i = 0; i < 3; i++)
            //    {
            //        var fieldId = $"sortCodeSet{i + 1}";
            //        _browserBot.WebDriver.FindElement(By.Id(fieldId)).SendKeys(sortcodeSplit[i]);
            //    }
            //}
            //else
            //{
            //    throw new InvalidOperationException("Could not determine login procedure from given properties.");
            //}

            //// Advance to stage 2
            //_browserBot.WebDriver.FindElement(By.Id("forward")).Click();

            //_browserBot.WebDriver.FindElement(By.Id("passcode-radio")).Click();
            //_browserBot.WebDriver.FindElement(By.Id("passcode")).SendKeys(_credentials.Passcode);

            //var passcodeCharElements = _browserBot.WebDriver
            //    .FindElement(By.ClassName("letter-select"))
            //    .FindElements(By.TagName("strong"));

            //var passcodeChar1 = _credentials.Passcode[passcodeCharElements[0].Text.AsInteger() - 1].ToString();
            //var passcodeChar2 = _credentials.Passcode[passcodeCharElements[1].Text.AsInteger() - 1].ToString();

            //_browserBot.WebDriver.FindElement(By.Id("nameOne")).SendKeys(passcodeChar1);
            //_browserBot.WebDriver.FindElement(By.Id("nameTwo")).SendKeys(passcodeChar1);

            //_browserBot.WebDriver.FindElement(By.Id("log-in-to-online-banking2")).Click();

            var scriptData = new Dictionary<string, string>
            {
                { "surname", _credentials.Surname },
                { "cardNumber", _credentials.CardNumber },
                { "sortCode", _credentials.SortCode },
                { "membershipNumber", _credentials.MembershipNumber },
                { "accountNumber", _credentials.AccountNumber }
            };

            scriptManager.Execute("barclays-login.js", scriptData, ScriptBundles.ProviderLogin);
        }
    }
}
