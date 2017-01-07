using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using automation_framework.framework;

namespace BaseAutomation.framework
{
    [Binding]
    public class Hooks
    {
        //More information about how hooks work can be found here. http://specflow.org/documentation/Hooks/

        [BeforeTestRun()]
        public static void BeforeAll()
        {
            Env.LoadConfig();
            Env.SetBrowser();
            Env.GetBrowser().Navigate().GoToUrl(Env.GetBaseUrl());
        }

        [AfterScenario]
        public static void AfterEach()
        {
            try
            {

                IWebDriver browser = Env.GetBrowser();
                browser.Manage().Cookies.DeleteAllCookies();
                browser.FindElement(By.Id("logout")).Click();
                browser.Manage().Cookies.DeleteAllCookies();
            }
            catch (Exception) { }
        }

        [AfterTestRun]
        private static void afterAll()
        {
            BrowserFactory.CloseAllDrivers();
        }
    }
}
