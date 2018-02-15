using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AutomationFramework.framework;

namespace BaseAutomation.framework
{
    public class Utils
    {
        public static void HighlightElement(IWebElement element)
        {
            IWebDriver driver = Env.GetBrowser();
            ((IJavaScriptExecutor)driver).ExecuteScript(@"$(arguments[0]).css({ ""border - width"" : ""2px"", ""border - style"" : ""solid"", ""border - color"" : ""red"" }); ",
                element);
        }
        
        public static void ScrollToElement(IWebElement element)
        {
            IWebDriver driver = Env.GetBrowser();
            var jsDriver = (IJavaScriptExecutor)driver;
            jsDriver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(500);
        }

        public static void ScrollByCoordinates(string x, string y)
        {
            IWebDriver driver = Env.GetBrowser();
            var jsDriver = (IJavaScriptExecutor)driver;
            jsDriver.ExecuteScript("window.scrollBy(arguments[0], arguments[1]);", x, y);
            Thread.Sleep(500);
        }
        
        public static WebDriverWait Wait(double timeout)
        {
            return new WebDriverWait(Env.GetBrowser(), TimeSpan.FromSeconds(timeout));
        }
    }
}
