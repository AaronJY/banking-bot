using OpenQA.Selenium;

namespace BankingBot.Extensions
{
    public static class IWebElementExtensions
    {
        public static bool HasElement(this IWebElement element, By by)
        {
            try
            {
                return element.FindElement(by) != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
