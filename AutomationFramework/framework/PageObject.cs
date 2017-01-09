using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using AutomationFramework.framework;

namespace BaseAutomation.framework
{
    /// <summary>
    /// This class is to be inherited by classes in the page_objects folder.
    /// </summary>
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
