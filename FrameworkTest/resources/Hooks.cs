using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using AutomationFramework.framework;
using RelevantCodes.ExtentReports;

namespace FrameworkTest.resources
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest test;
        //More information about how hooks work can be found here. http://specflow.org/documentation/Hooks/

        [BeforeTestRun()]
        public static void BeforeAll()
        {
            Env.LoadConfig();
            Env.SetBrowser();
            Env.InitializeExtentReport();
            Env.GetBrowser().Navigate().GoToUrl(Env.GetBaseUrl());
            Env.GetExtentReport().AddSystemInfo("Browser", Env.config.browser);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {

        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            //Get the scenario title to pass in for the test logging
            string testName = ScenarioContext.Current.ScenarioInfo.Title;

            //Start logging for the scenario using the scenario title
            test = Env.GetExtentReport().StartTest(testName);
        }

        [BeforeStep]
        public void BeforeStep()
        {
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepName = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text;
            var error = ScenarioContext.Current.TestError;

            if (ScenarioContext.Current.TestError != null)
            {
                test.Log(LogStatus.Info, stepName, error);
            }
            else
            {
                test.Log(LogStatus.Info, stepName);
            }
        }

        [AfterScenario]
        public static void AfterEach()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                var error = ScenarioContext.Current.TestError;
                test.Log(LogStatus.Fail, error);
            }
            else
            {
                var scenarioName = ScenarioContext.Current.ScenarioInfo.Title;
                test.Log(LogStatus.Pass, scenarioName);
            }

            try
            {

                IWebDriver browser = Env.GetBrowser();
                browser.Manage().Cookies.DeleteAllCookies();
                browser.FindElement(By.Id("logout")).Click();
                browser.Manage().Cookies.DeleteAllCookies();
            }
            catch (Exception) { }
        }

        [AfterFeature]
        public static void AfterFeature()
        {

        }

        [AfterTestRun]
        private static void AfterAll()
        {
            Env.GetExtentReport().Flush();
            Env.ClearConfig();
            BrowserFactory.CloseAllDrivers();
        }
    }
}
