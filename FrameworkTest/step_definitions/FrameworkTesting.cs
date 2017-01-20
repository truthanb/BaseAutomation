using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseAutomation.framework;
using AutomationFramework.framework;
using FrameworkTest.page_objects;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace FrameworkTest.step_definitions
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

        [Then(@"Verify successful login")]
        public void blahp()
        {
            HerokuSecurePage page = new HerokuSecurePage();
            driver.WaitForPageObject(page.successAlertMsg, 10);
            Assert.AreEqual("You logged into a secure area!\r\n×", page.successAlertMsg.Text);
            page.alertCloseButton.Click(TimeSpan.FromSeconds(2));
            Assert.AreEqual(0, driver.FindElements(By.CssSelector("div.flash.success")).Count);
            page.logoutButton.Click();
        }

        [Then(@"Fail Intentionally")]
        public void ThenFailIntentionally()
        {
            Assert.AreEqual(1, 2, "Expected one to equal 2, but nunit says, 'No, it doesn't equal 2.'");
        }

    }
}
