using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseAutomation.framework;
using AutomationFramework.framework;
using FrameworkTest.page_objects;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow.Assist;
using System.Threading;

namespace FrameworkTest.step_definitions
{
    [Binding]
    public sealed class FrameworkTesting
    {
        private IWebDriver _driver;
        public FrameworkTesting(IWebDriver driver)
        {
            _driver = driver;
        }
        [Given(@"Login to heroku app")]
        public void GivenIHaveGoneToHerokuApp(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            HerokuLoginPage page = new HerokuLoginPage(_driver);
            page.userNameInput.SendKeys(data.UserName);
            page.passwordInput.SendKeys(data.Password);
            page.submitButton.Click();
        }

        [Then(@"Verify successful login")]
        public void blahp()
        {
            HerokuSecurePage page = new HerokuSecurePage(_driver);
            _driver.WaitForPageObject(page.successAlertMsg, 10);
            Assert.AreEqual("You logged into a secure area!\r\n×", page.successAlertMsg.Text);
            page.alertCloseButton.Click(TimeSpan.FromSeconds(2));
            Assert.AreEqual(0, _driver.FindElements(By.CssSelector("div.flash.success")).Count);
            Thread.Sleep(20000);
            page.logoutButton.Click();
        }

        [Then(@"Fail Intentionally")]
        public void ThenFailIntentionally()
        {
            Assert.AreEqual(1, 2, "Expected one to equal 2, but nunit says, 'No, it doesn't equal 2.'");
        }

    }
}