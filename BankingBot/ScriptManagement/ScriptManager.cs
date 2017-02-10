using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace BankingBot.ScriptManagement
{
    public class ScriptManager
    {
        private readonly IBrowserBot _browserBot;

        public ScriptManager(IBrowserBot browserBot)
        {
            _browserBot = browserBot;
        }

        public T Execute<T>(string scriptPath, Dictionary<string, string> data = null, string[] includedScripts = null)
        {
            var scriptContent = "";

            if (includedScripts != null)
            {
                foreach (var script in includedScripts)
                {
                    var includedScriptContent = File.ReadAllText(script).Trim();
                    scriptContent += includedScriptContent;
                }
            }

            scriptContent += File.ReadAllText(scriptPath).Trim();

            if (data != null)
            {
                var placeholderFormat = "__${0}";
                foreach (var pair in data)
                {
                    var placeholderText = string.Format(placeholderFormat, pair.Key);
                    scriptContent = scriptContent.Replace(placeholderText, pair.Value);
                }
            }

            var executor = _browserBot.WebDriver as IJavaScriptExecutor;
            return (T)executor.ExecuteScript(scriptContent);
        }
    }
}
