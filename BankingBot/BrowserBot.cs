using System;
using BankingBot.Contracts;
using OpenQA.Selenium;

namespace BankingBot
{
    public class BrowserBot<T> : IBrowserBot, IDisposable
        where T : IWebDriver
    {
        public IWebDriver WebDriver { get; private set; }

        public BrowserBot()
        {
            WebDriver = (IWebDriver)Activator.CreateInstance(typeof(T));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WebDriver.Quit();
                }

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BrowserBot() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
