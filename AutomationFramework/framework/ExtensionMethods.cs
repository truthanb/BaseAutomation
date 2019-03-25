using System;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.ObjectModel;

namespace BaseAutomation.Framework
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Easier to use Wait for element method.  Only selects one element.
        /// </summary>
        /// <param name="me">Current Web Driver</param>
        /// <param name="by">What are you looking for?</param>
        /// <param name="seconds">How long do you want to wait for it?</param>
        /// <returns>One element that you were looking for.</returns>
        public static IWebElement WaitForElement(this IWebDriver me, By by, int seconds)
        {
            var milliWait = seconds * 1000;
            var iterations = milliWait / 250;
            IWebElement element = null;
            for (int i = 0; i < iterations; i++)
            {
                try
                {
                    element = me.FindElement(by);
                }
                catch { }
                if (element != null)
                    return element;
                Thread.Sleep(250);
            }
            throw new Exception("The element you are looking for did not appear. " + by.ToString());
        }

        /// <summary>
        /// Similar to WaitForElement, but takes an already defined page object. If the element can be hovered over, 
        /// then the test can proceed. If the method times out, an exception is thrown.
        /// </summary>
        /// <param name="driver">Current Web Driver</param>
        /// <param name="element">Desired Page Object.</param>
        /// <param name="seconds">Desired wait limit in seconds.</param>
        public static void WaitForPageObject(this IWebDriver driver, IWebElement element, int seconds)
        {
            var milliWait = seconds * 1000;
            var iterations = milliWait / 250;
            for (int i = 0; i < iterations; i++)
            {
                try
                {
                    element.HoverOverElement(driver);
                    return;
                }
                catch { }
                Thread.Sleep(250);
            }
            throw new Exception("The element you are looking for did not appear. " + element.ToString());
        }

        /// <summary>
        /// Forces a delay after clicking a web element.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="time"></param>
        public static void Click(this IWebElement me, TimeSpan time)
        {
            me.Click();
            Thread.Sleep(time);
        }

        /// <summary>
        /// Picks a random element from a collection of elements.
        /// </summary>
        /// <param name="me"></param>
        /// <returns>A random choice from a collection of elements.</returns>
        public static IWebElement RandomChoice(this ReadOnlyCollection<IWebElement> me)
        {
            var rnd = new Random();
            var rndXpath = rnd.Next(0, (me.Count - 1));
            return me[rndXpath];
        }

        public static IWebElement FindDisplayed(this ReadOnlyCollection<IWebElement> me)
        {
            foreach (var element in me)
                if (element.Displayed)
                    return element;
            return null;
        }

        /// <summary>
        /// Mimics mouse action of hovering an element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driver"></param>
        public static void HoverOverElement(this IWebElement element, IWebDriver driver)
        {
            Actions act = new Actions(driver);
            act.MoveToElement(element).Build().Perform();
        }

        public static bool ElementIsPresent(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Finds the hex value for a given rgb string. Getting element attribute for color returns rgb value, but hex value is usually is more useful.
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static string ConvertRGBToHex(this string rgb)
        {
            var numbers = rgb.Contains("a") ?
                rgb.Replace("rgba(", "").Replace(")", "").Split(',')
                : rgb.Replace("rgb(", "").Replace(")", "").Split(',');

            int r = Convert.ToInt16(numbers[0].Trim());
            int g = Convert.ToInt16(numbers[1].Trim());
            int b = Convert.ToInt16(numbers[2].Trim());
            var hex = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
            return hex;
        }
    }
}
