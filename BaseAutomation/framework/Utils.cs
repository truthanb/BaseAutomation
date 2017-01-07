using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using automation_framework.framework;

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
    }
}
