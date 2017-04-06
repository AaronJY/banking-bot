using OpenQA.Selenium;

namespace BankingBot.Extensions
{
    public static class IWebElementExtensions
    {
        public static bool HasElement(this IWebElement element, By by)
        {
            try
            {
                element.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
