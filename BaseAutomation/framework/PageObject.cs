using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using automation_framework.framework;

namespace BaseAutomation.framework
{
    public abstract class PageObject
    {
        protected IWebDriver browser;

        protected PageObject()
        {
            browser = Env.GetBrowser();
            PageFactory.InitElements(browser, this);
        }
    }
}
