using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BankingBot.Attributes;
using BankingBot.Contracts;
using BankingBot.LoginCredentials;
using OpenQA.Selenium;
using BankingBot.Responses;
using BankingBot.Enums;
using BankingBot.ScriptManagement;
using BankingBot.Urls;
using BankingBot.Exceptions;

namespace BankingBot.ActionManagers.LoginManagers
{
    [ProviderIdentifier(Provider.Lloyds)]
    public class LloydsLoginManager : IProviderLoginManager
    {
        private readonly IBrowserBot _browserBot;
        private readonly IScriptManager _scriptManager;
        
        public LloydsLoginManager(
            IBrowserBot browserBot,
            IScriptManager scriptManager)
        {
            _browserBot = browserBot;
            _scriptManager = scriptManager;
        }

        public Response Login(ILoginCredentials credentials)
        {
            var response = new Response();
            var lloydsCreds = (LloydsLoginCredentials)credentials;

            try
            {
                LoginStep1(lloydsCreds);

                if (!_browserBot.WebDriver.Url.Contains(LloydsUrls.MemorableInfo))
                    throw new InvalidCredentialsException("Invalid login credentials");

                LoginStep2(lloydsCreds);

                if (!_browserBot.WebDriver.Url.Contains(LloydsUrls.AccountOverview))
                    throw new InvalidCredentialsException("Invalid passphrase for account");

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                response.Status = ResponseStatus.Error;
            }

            return response;
        }

        private void LoginStep1(LloydsLoginCredentials credentials)
        {
            _browserBot.WebDriver.Url = LloydsUrls.Login;
            _browserBot.WebDriver.Navigate();

            _browserBot.WebDriver.FindElement(By.Id("frmLogin:strCustomerLogin_userID")).SendKeys(credentials.Username);
            _browserBot.WebDriver.FindElement(By.Id("frmLogin:strCustomerLogin_pwd")).SendKeys(credentials.Password);

            _browserBot.WebDriver.FindElement(By.Id("frmLogin:btnLogin2")).Click();
        }

        private void LoginStep2(LloydsLoginCredentials credentials)
        {
            var passphraseIndexes = GetPassphraseIndexes();

            var maxPassphraseLength = passphraseIndexes[2];
            if (credentials.Passphrase.Length < maxPassphraseLength)
                throw new InvalidCredentialsException("Passphrase is too short");

            _browserBot.WebDriver.FindElement(By.Id(GetPassphraseDdlId(1))).SendKeys(
                credentials.Passphrase[passphraseIndexes[0]].ToString());

            _browserBot.WebDriver.FindElement(By.Id(GetPassphraseDdlId(2))).SendKeys(
                credentials.Passphrase[passphraseIndexes[1]].ToString());

            _browserBot.WebDriver.FindElement(By.Id(GetPassphraseDdlId(3))).SendKeys(
                credentials.Passphrase[passphraseIndexes[2]].ToString());

            _browserBot.WebDriver.FindElement(By.Id("frmentermemorableinformation1:btnContinue")).Click();
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
