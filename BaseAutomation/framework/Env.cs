using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using BaseAutomation.framework;
using Newtonsoft.Json;
using System.IO;

namespace automation_framework.framework
{
    public sealed class Env
    {
        private static IWebDriver browser;

        private static Configuration config = new Configuration();
        private static int timeOutDuration = 10;
        private static Boolean configLoaded = false;

        private Env() { }

        public static String GetBaseUrl()
        {
            return config.base_url;
        }

        public static IWebDriver GetBrowser()
        {
                return browser;
        }

        public static IWebDriver SetBrowser()
        {
            LoadConfig();
            string browserType = config.browser;
            if (config.browser.ToLower() == "no browser")
            { 
                BrowserFactory.CloseAllDrivers();
                return null;
            }
            browser = BrowserFactory.GetBrowser(browserType);
            SetTimeOutDuration(timeOutDuration);
            return browser;
        }

        public static IWebDriver SetBrowser(string browserType)
        {
            LoadConfig();
            if (config.browser.ToLower() == "no browser")
            {
                BrowserFactory.CloseAllDrivers();
                return null;
            }
            browser = BrowserFactory.GetBrowser(browserType);
            SetTimeOutDuration(timeOutDuration);
            return browser;
        }

        private static void SetTimeOutDuration(int tod)
        {
            browser.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(tod));
        }

        public static void LoadConfig()
        {
            if (configLoaded)
            {
                return;
            }
            try
            {
                string configPath = @"C:\Users\Ben\documents\visual studio 2015\Projects\BaseAutomation\BaseAutomation\resources\data\configuration.json";
                using (StreamReader r = new StreamReader(configPath))
                {
                    string json = r.ReadToEnd();
                    config = JsonConvert.DeserializeObject<Configuration>(json);
                    timeOutDuration = config.time_out_duration;
                    configLoaded = true;
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Could not load config file. Using default settings.");
            }
        }
    }
}
