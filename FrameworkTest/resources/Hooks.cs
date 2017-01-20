using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using AutomationFramework.framework;
using RelevantCodes.ExtentReports;

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
            Env.InitializeExtentReport();
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            Env.BeginExtentTestLogging(ScenarioContext.Current.ScenarioInfo.Title, "sample");
        }

        [BeforeStep()]
        public static void BeforeStep()
        {

        }

        [AfterStep()]
        public static void AfterStep()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                var error = ScenarioContext.Current.TestError;
                Env.test.Log(LogStatus.Error, ScenarioContext.Current.StepContext.StepInfo.StepDefinitionType +" "+
                    ScenarioContext.Current.StepContext.StepInfo.Text ,error.Message + error.Source + error.StackTrace);
            }
            else
            {
                string message = "Test Step: " + ScenarioContext.Current.StepContext.StepInfo.Text + "PASSED";
                Env.test.Log(LogStatus.Pass, message);
            }
        }

        [AfterScenario]
        public static void AfterEach()
        {
            Env.EndExtentTest();
            RecordTestResult();

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
            Env.ClearConfig();
            BrowserFactory.CloseAllDrivers();
            Env.WriteExtentReport();
        }

        public static void RecordTestResult()
        {
            
            if (ScenarioContext.Current.TestError != null)
            {
                var error = ScenarioContext.Current.TestError;
                Env.test.Log(LogStatus.Fail, error.GetBaseException());
            }
            else
            {
                string message = "Scenario: " + ScenarioContext.Current.ScenarioInfo.Title + " PASSED";
                Env.test.Log(LogStatus.Pass, message);
            }
        }
    }
}
