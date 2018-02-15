using Newtonsoft.Json;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.IO;

namespace AutomationFramework.framework
{
    /// <summary>
    /// Env is essentially the global variable class.
    /// </summary>
    public sealed class Env
    {
        private static IWebDriver browser;

        public static Configuration config;
        private static int timeOutDuration = 10;
        private static Boolean configLoaded = false;
        private static ExtentReports extent;

        private Env()
        {
        }

        public static string GetBaseUrl()
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
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(tod);
        }

        /// <summary>
        /// Loads a config file for relevant test run parameters.
        /// </summary>
        public static void LoadConfig()
        {
            if (configLoaded)
            {
                return;
            }
            try
            {
                config = new Configuration();
                DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                var configPath = Path.Combine(dir.Parent.Parent.FullName, @"resources\configuration.json");
                using (StreamReader r = new StreamReader(configPath))
                {
                    string json = r.ReadToEnd();
                    config = JsonConvert.DeserializeObject<Configuration>(json);
                    timeOutDuration = config.time_out_duration;
                    configLoaded = true;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Could not load config file. Using default settings. {0}", e);
            }
        }

        /// <summary>
        /// Setting configLoaded to false will make loadConfig() load the config file. Good to run at the end of a Test run...
        /// when tests spanning multiple projects are run.
        /// </summary>
        public static void ClearConfig()
        {
            configLoaded = false;
        }

        public static void InitializeExtentReport()
        {
            string dir = config.report_path;
            var reportPath = Path.Combine(dir, config.browser + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss") + ".html");
            extent = new ExtentReports(reportPath);
        }

        public static ExtentReports GetExtentReport()
        {
            return extent;
        }
    }
}
