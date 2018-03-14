using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using AutomationFramework.Framework;
using BoDi;
using System.Configuration;
using System.Threading;

namespace FrameworkTest.resources
{
    [Binding]
    public class Hooks
    {
        //More information about how hooks work can be found here. http://specflow.org/documentation/Hooks/


        private readonly IObjectContainer _objectContainer;

        private IWebDriver _driver;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }


        [BeforeScenario]
        public void BeforeScenario()
        {

            Thread.Sleep(1500);
            _driver = BrowserFactory.GetBrowser(ConfigurationManager.AppSettings["Browser"].ToString());
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["BaseUrl"].ToString());

        }

        [BeforeStep]
        public void BeforeStep()
        {
        }

        [AfterStep]
        public void AfterStep()
        {

        }

        [AfterScenario]
        public void AfterEach()
        {
            try
            {
                Thread.Sleep(3000);
                _driver.Quit();
            }
            catch (Exception) { }
        }
    }
}
