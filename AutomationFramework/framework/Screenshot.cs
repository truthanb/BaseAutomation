using OpenQA.Selenium;
using System;
using System.Text;

namespace AutomationFramework.framework
{
    public static class Screenshot
    {
        public static string SaveScreenshot()
        {
            if (!System.IO.Directory.Exists(Env.config.screenshot_path))
            {
                System.IO.Directory.CreateDirectory(Env.config.screenshot_path);
            }

            var screenShot = ((ITakesScreenshot)Env.GetBrowser()).GetScreenshot();
            var fileName = new StringBuilder(Env.config.screenshot_path);

            fileName.Append("ScreenCapture" + DateTime.Now.ToString("_dd-mm-yyyy_mss") + ".png");

            screenShot.SaveAsFile(fileName.ToString(), System.Drawing.Imaging.ImageFormat.Png);

            return fileName.ToString();
        }
    }
}