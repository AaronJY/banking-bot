using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Contracts;
using OpenQA.Selenium;

namespace BankingBot
{
    public class BrowserBot<T> : IBrowserBot
        where T : IWebDriver
    {
        public IWebDriver WebDriver { get; private set; }

        public BrowserBot()
        {
            WebDriver = (IWebDriver)Activator.CreateInstance(typeof(T));
        }
    }
}
