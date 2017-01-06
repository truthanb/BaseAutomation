using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using BaseAutomation.framework;
using BaseAutomation.page_objects;
using System.Threading;




namespace BaseAutomation.step_definitions
{
    [Binding]
    public sealed class FrameworkTesting
    {

        [Given(@"I have gone to heroku app")]
        public void GivenIHaveGoneToHerokuApp()
        {
            HerokuLoginPage page = new HerokuLoginPage();
            page.userNameInput.SendKeys("tomsmith");
            page.passwordInput.SendKeys("SuperSecretPassword!");
            page.submitButton.Click();
        }

        [Then(@"the result should be 120 on the screen")]
        public void blahp()
        {
            Thread.Sleep(10000);
        }

    }
}
