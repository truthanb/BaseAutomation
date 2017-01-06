using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseAutomation.framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BaseAutomation.page_objects
{
    public class HerokuLoginPage : PageObject
    {
        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement userNameInput;

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement passwordInput;

        [FindsBy(How = How.XPath, Using = "//button[@class = 'radius']/i")]
        public IWebElement submitButton;
    }
}
