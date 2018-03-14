using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FrameworkTest.PageObjects
{
    public class HerokuLoginPage
    {
        public HerokuLoginPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement UserNameInput;

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement PasswordInput;

        [FindsBy(How = How.XPath, Using = "//button[@class = 'radius']/i")]
        public IWebElement SubmitButton;
    }
}
