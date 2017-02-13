using System;
using System.Text.RegularExpressions;
using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.LoginCredentials;
using OpenQA.Selenium;
using BankingBot.Enums;
using BankingBot.Urls;
using BankingBot.Exceptions;

namespace BankingBot.ActionManagers.LoginManagers
{
    [ProviderIdentifier(Provider.Lloyds)]
    public class LloydsLoginManager : IProviderLoginManager
    {
        readonly IBrowserBot _browserBot;
        readonly IScriptManager _scriptManager;

        private LloydsLoginCredentials _credentials;
        
        public LloydsLoginManager(
            IBrowserBot browserBot,
            IScriptManager scriptManager)
        {
            _browserBot = browserBot;
            _scriptManager = scriptManager;
        }

        public void Login(ILoginCredentials credentials)
        {
            _credentials = (LloydsLoginCredentials)credentials;

            LoginStep1();
            LoginStep2();
        }

        private void LoginStep1()
        {
            _browserBot.WebDriver.Url = LloydsUrls.Login;
            _browserBot.WebDriver.Navigate();

            _browserBot.WebDriver.FindElement(By.Id("frmLogin:strCustomerLogin_userID")).SendKeys(_credentials.Username);
            _browserBot.WebDriver.FindElement(By.Id("frmLogin:strCustomerLogin_pwd")).SendKeys(_credentials.Password);

            _browserBot.WebDriver.FindElement(By.Id("frmLogin:btnLogin2")).Click();

            if (!_browserBot.WebDriver.Url.Contains(LloydsUrls.MemorableInfo))
                throw new InvalidCredentialsException("Invalid login credentials");
        }

        private void LoginStep2()
        {
            var passphraseIndexes = GetPassphraseIndexes();

            var maxPassphraseLength = passphraseIndexes[2];
            if (_credentials.Passphrase.Length < maxPassphraseLength)
                throw new InvalidCredentialsException("Passphrase is too short");

            _browserBot.WebDriver.FindElement(By.Id(GetPassphraseDdlId(1))).SendKeys(
                _credentials.Passphrase[passphraseIndexes[0]].ToString());

            _browserBot.WebDriver.FindElement(By.Id(GetPassphraseDdlId(2))).SendKeys(
                _credentials.Passphrase[passphraseIndexes[1]].ToString());

            _browserBot.WebDriver.FindElement(By.Id(GetPassphraseDdlId(3))).SendKeys(
                _credentials.Passphrase[passphraseIndexes[2]].ToString());

            _browserBot.WebDriver.FindElement(By.Id("frmentermemorableinformation1:btnContinue")).Click();

            if (!_browserBot.WebDriver.Url.Contains(LloydsUrls.AccountOverview))
                throw new InvalidCredentialsException("Invalid passphrase for account");
        }

        private int[] GetPassphraseIndexes()
        {
            if (_browserBot.WebDriver.Url != LloydsUrls.MemorableInfo)
                throw new InvalidOperationException("Must be on the memorable info page");

            var charIndexes = new int[3];
            for (var i = 0; i < 3; i++)
            {
                var cssSelector = $"label[for='{GetPassphraseDdlId(i + 1)}']";
                var labelText = _browserBot.WebDriver.FindElement(By.CssSelector(cssSelector)).Text;
                var labelIndex = int.Parse(Regex.Replace(labelText, "[^0-9]", ""));

                charIndexes[i] = labelIndex;
            }

            return charIndexes;
        }

        private string GetPassphraseDdlId(int index)
        {
            if (index < 1 || index > 3)
                throw new ArgumentException("Must be between 1 and 3");

            return $"frmentermemorableinformation1:strEnterMemorableInformation_memInfo{index}";
        }
    }
}
