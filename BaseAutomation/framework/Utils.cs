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
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]", element, "background: yellow; border: 2px solid red;");
        }
    }
}
