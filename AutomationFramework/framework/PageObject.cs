using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using AutomationFramework.framework;

namespace BaseAutomation.framework
{
    public abstract class PageObject
    {
        //This class is to be inherited by classes in the page_objects folder.
        protected IWebDriver browser;

        protected PageObject()
        {
            browser = Env.GetBrowser();
            PageFactory.InitElements(browser, this);
        }
    }
}
