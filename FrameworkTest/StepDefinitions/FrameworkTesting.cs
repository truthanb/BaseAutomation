using BaseAutomation.Framework;
using FrameworkTest.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace FrameworkTest.StepDefinitions
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
            page.UserNameInput.SendKeys(data.UserName);
            page.PasswordInput.SendKeys(data.Password);
            page.SubmitButton.Click();
        }

        [Then(@"Verify successful login")]
        public void blahp()
        {
            HerokuSecurePage page = new HerokuSecurePage(_driver);
            _driver.WaitForPageObject(page.SuccessAlertMsg, 10);
            Assert.AreEqual("You logged into a secure area!\r\n×", page.SuccessAlertMsg.Text);
            page.AlertCloseButton.Click(TimeSpan.FromSeconds(2));
            Assert.AreEqual(0, _driver.FindElements(By.CssSelector("div.flash.success")).Count);
            page.LogoutButton.Click();
        }

        [Then(@"Fail Intentionally")]
        public void ThenFailIntentionally()
        {
            Assert.AreEqual(1, 2, "Expected one to equal 2, but nunit says, 'No, it doesn't equal 2.'");
        }
    }
}