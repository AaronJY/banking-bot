using System;
using System.Collections.Generic;
using System.IO;
using BankingBot.Contracts;
using OpenQA.Selenium;

namespace BankingBot.ScriptManagement
{
    public class ScriptManager : IScriptManager
    {
        private readonly IBrowserBot _browserBot;

        public ScriptManager(IBrowserBot browserBot)
        {
            _browserBot = browserBot;
        }

        public T Execute<T>(string scriptPath, Dictionary<string, string> data = null, string[] includedScripts = null)
        {
            var script = GenerateScript(scriptPath, data, includedScripts);
            var executor = _browserBot.WebDriver as IJavaScriptExecutor;
            return (T)executor.ExecuteScript(script);
        }

        public void Execute(string scriptPath, Dictionary<string, string> data, string[] includedScripts = null)
        {
            var script = GenerateScript(scriptPath, data, includedScripts);
            var executor = _browserBot.WebDriver as IJavaScriptExecutor;
            executor.ExecuteScript(script);
        }

        private string GetIncludedScriptsCompilation(string currentCompliation, string[] scripts)
        {
            if (scripts != null)
            {
                foreach (var script in scripts)
                {
                    currentCompliation += GetScriptContent(script);
                }
            }

            return currentCompliation;
        }

        private string GetScriptContent(string scriptPath)
        {
            return File.ReadAllText(scriptPath).Trim() + Environment.NewLine;
        }

        private string GetScriptWithPopulatedData(string currentCompliation, Dictionary<string, string> data)
        {
            if (data != null)
            {
                var placeholderFormat = "__${0}";
                foreach (var pair in data)
                {
                    var placeholderText = string.Format(placeholderFormat, pair.Key);
                    currentCompliation = currentCompliation.Replace(placeholderText, $"\"{pair.Value}\"");
                }
            }

            return currentCompliation;
        }

        private string GenerateScript(string scriptPath, Dictionary<string, string> data = null, string[] includedScripts = null)
        {
            var scriptContent = GetIncludedScriptsCompilation("", includedScripts);
            scriptContent += GetScriptContent(scriptPath);
            scriptContent = GetScriptWithPopulatedData(scriptContent, data);

            return scriptContent;
        }
    }
}
