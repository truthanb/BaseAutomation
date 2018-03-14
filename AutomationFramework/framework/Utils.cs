using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace BaseAutomation.Framework
{
    public class Utils
    {
        public static void HighlightElement(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(@"$(arguments[0]).css({ ""border - width"" : ""2px"", ""border - style"" : ""solid"", ""border - color"" : ""red"" }); ",
                element);
        }
        
        public static void ScrollToElement(IWebDriver driver, IWebElement element)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            jsDriver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(500);
        }

        public static void ScrollByCoordinates(IWebDriver driver, string x, string y)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            jsDriver.ExecuteScript("window.scrollBy(arguments[0], arguments[1]);", x, y);
            Thread.Sleep(500);
        }
        
        public static WebDriverWait Wait(IWebDriver driver, double timeout)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
        }
    }
}
