using System;
using OpenQA.Selenium;
using BaseAutomation.framework;
using Newtonsoft.Json;
using System.IO;
using RelevantCodes.ExtentReports;

namespace AutomationFramework.framework
{
    /// <summary>
    /// Env is essentially the global variable class.
    /// </summary>
    public sealed class Env
    {
        private static IWebDriver browser;

        private static Configuration config;
        private static int timeOutDuration = 10;
        private static Boolean configLoaded = false;
        public static ExtentReports report;
        public static ExtentTest test;

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
            catch(IOException e)
            {
                Console.WriteLine("Could not load config file. Using default settings.");
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

        /// <summary>
        /// 
        /// </summary>
        public static void InitializeExtentReport()
        {
            report = new ExtentReports(config.report_path, true);
            report.AddSystemInfo("Browser", config.browser);
        }

        public static void WriteExtentReport()
        {
            report.Flush();
        }

        public static void BeginExtentTestLogging(string title, string descr)
        {
            test = report.StartTest(title, descr);
        }

        public static void EndExtentTest()
        {
            report.EndTest(test);
        }
    }
}
