using TechTalk.SpecFlow;
using BaseAutomation.framework;
using BaseAutomation.page_objects;
using System.Threading;

namespace BaseAutomation.step_definitions
{
    [Binding]
    public sealed class FrameworkTesting
    {

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
            Thread.Sleep(10000);
        }
    }
}
