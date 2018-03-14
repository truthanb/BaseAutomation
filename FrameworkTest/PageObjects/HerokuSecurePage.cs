using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FrameworkTest.PageObjects
{
    class HerokuSecurePage
    {
        public HerokuSecurePage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "div.flash.success")]
        public IWebElement SuccessAlertMsg { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a.close")]
        public IWebElement AlertCloseButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@href = '/logout']")]
        public IWebElement LogoutButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "h4.subheader")]
        public IWebElement WelcomeMsg { get; set; }
    }
}
