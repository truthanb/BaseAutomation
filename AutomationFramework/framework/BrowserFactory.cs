using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using System.IO;



namespace AutomationFramework.framework
{
    public class BrowserFactory
    {
        private static Dictionary<string, IWebDriver> drivers = new Dictionary<string, IWebDriver>();

        public static IWebDriver GetBrowser(String browserName)
        {
            IWebDriver driver = null;
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var pathToDrivers = Path.Combine(dir.Parent.Parent.Parent.FullName, @"AutomationFramework\resources\browser_drivers");

            switch (browserName)
            {
                case "Firefox":
                        drivers.TryGetValue("Firefox", out driver);
                        if (driver == null)
                        {
                        FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                        service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

                        driver = new FirefoxDriver(service);
                            drivers.Add("Firefox", driver);
                            driver.Manage().Window.Maximize();
                            return driver;
                        }
                    else
                    {
                        return driver;
                    }
                case "IE":
                    {
                        drivers.TryGetValue("IE", out driver);
                        if (driver == null)
                        {
                            var options = new InternetExplorerOptions();
                            options.EnsureCleanSession = true;
                            driver = new InternetExplorerDriver(pathToDrivers, options);
                            drivers.Add("IE", driver);
                            driver.Manage().Window.Maximize();
                            return driver;
                        }
                        else
                        {
                            return driver;
                        }
                    }
                case "Chrome":
                    {
                        drivers.TryGetValue("Chrome", out driver);
                        if (driver == null)
                        {
                            driver = new ChromeDriver(pathToDrivers);
                            drivers.Add("Chrome", driver);
                            driver.Manage().Window.Maximize();
                            return driver;
                        }
                        else
                        {
                            return driver;
                        }
                    }
                case "Edge":
                    {
                        var browserPath = Environment.CurrentDirectory.Replace(@"\bin\Debug", @"\resources\browser_drivers\MicrosoftWebDriver.exe");
                        Environment.SetEnvironmentVariable("webdriver.edge.driver", browserPath);
                        drivers.TryGetValue("Edge", out driver);
                        if (driver == null)
                        {
                            driver = new EdgeDriver(pathToDrivers);
                            drivers.Add("Edge", driver);
                            driver.Manage().Window.Maximize();
                            return driver;
                        }
                        else
                        {
                            return driver;
                        }
                    }
                case "Headless":
                case "no browser":
                    return null;
                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        } 

        public static void CloseAllDrivers()
        {
            foreach(string key in drivers.Keys)
            {
                IWebDriver driver;
                drivers.TryGetValue(key, out driver);
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
