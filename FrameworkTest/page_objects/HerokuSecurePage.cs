using BaseAutomation.framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace FrameworkTest.page_objects
{
    class HerokuSecurePage
    {
        public HerokuSecurePage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "div.flash.success")]
        public IWebElement successAlertMsg;

        [FindsBy(How = How.CssSelector, Using = "a.close")]
        public IWebElement alertCloseButton;

        [FindsBy(How = How.XPath, Using = "//a[@href = '/logout']")]
        public IWebElement logoutButton;

        [FindsBy(How = How.CssSelector, Using = "h4.subheader")]
        public IWebElement WelcomeMsg;
    }
}
