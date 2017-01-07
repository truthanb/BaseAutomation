using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseAutomation.framework;
using automation_framework.framework;
using BaseAutomation.page_objects;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace BaseAutomation.step_definitions
{
    [Binding]
    public sealed class FrameworkTesting
    {
        IWebDriver driver = Env.GetBrowser();

        [Given(@"Login to heroku app")]
        public void GivenIHaveGoneToHerokuApp()
        {

            HerokuLoginPage page = new HerokuLoginPage();
            page.userNameInput.SendKeys("tomsmith");
            page.passwordInput.SendKeys("SuperSecretPassword!");
            page.submitButton.Click();
        }

        [Then(@"Watch time go by")]
        public void blahp()
        {
            HerokuSecurePage page = new HerokuSecurePage();
            driver.WaitForPageObject(page.successAlertMsg, 10);
            Assert.AreEqual("You logged into a secure area!\r\n×", page.successAlertMsg.Text);
            page.alertCloseButton.Click(TimeSpan.FromSeconds(2));
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div.flash.success")).Count);
            page.logoutButton.Click();
        }
    }
}
