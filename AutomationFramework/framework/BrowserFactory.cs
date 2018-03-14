using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;

namespace AutomationFramework.Framework
{
    public class BrowserFactory
    {
        public static IWebDriver GetBrowser(String browserName)
        {
            IWebDriver driver = null;
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var pathToDrivers = Path.Combine(dir.Parent.Parent.Parent.FullName, @"AutomationFramework\BrowserDrivers");

            switch (browserName.ToLower())
            {
                case "firefox":
                    {
                        FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(pathToDrivers, "geckodriver.exe");
                        service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                        driver = new FirefoxDriver(service);
                        driver.Manage().Window.Maximize();
                        return driver;
                    }
                case "ie":
                    {
                        var options = new InternetExplorerOptions();
                        options.EnsureCleanSession = true;
                        options.ElementScrollBehavior = InternetExplorerElementScrollBehavior.Bottom;
                        driver = new InternetExplorerDriver(pathToDrivers, options);
                        driver.Manage().Window.Maximize();
                        return driver;
                    }
                case "chrome":
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments("--test-type");
                        chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                        chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOptions.AddArguments("disable-infobars");
                        chromeOptions.AddArguments("start-maximized");
                        driver = new ChromeDriver(pathToDrivers, chromeOptions);
                        return driver;
                    }
                case "edge":
                    {
                        var browserPath = Environment.CurrentDirectory.Replace(@"\bin\Debug", @"\resources\browser_drivers\MicrosoftWebDriver.exe");
                        Environment.SetEnvironmentVariable("webdriver.edge.driver", browserPath);
                        var options = new EdgeOptions();
                        driver = new EdgeDriver(pathToDrivers, options);
                        driver.Manage().Window.Maximize();
                        return driver;
                    }
                case "headless":
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments("--headless");
                        chromeOptions.AddArguments("--test-type");
                        chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                        chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOptions.AddArguments("disable-infobars");
                        chromeOptions.AddArguments("start-maximized");
                        driver = new ChromeDriver(pathToDrivers, chromeOptions);
                        return driver;
                    }
                case "no browser":
                    return null;

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }
    }
}