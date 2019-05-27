using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Tests_For_TestInfrastructure_Course.app;
using Tests_For_TestInfrastructure_Course.config;
using System.Net;
using System.IO;
using System.Net.Http;

namespace Tests_For_TestInfrastructure_Course.pages
{
    public class FiledownloaderPage: BasePage
    {
        public FiledownloaderPage(Application app) : base(app) { }

        public void Open()
        {
            Log.Logger.Information($"Browser is going to url: {TestSettings.ToDoApplicationUrl.ToString()}");
            Driver.Url = TestSettings.DownloadAppUrl.ToString();
        }

        public void DownloadFirstItem()
        {
            var element = Driver.FindElements(By.CssSelector("a"))[1];
            element.Click();
            var fileName = element.Text;
            this.Wait();

            var url = $"http://localhost:4444/download/{GetSessionId()}/{fileName}";
            GetFileFromSelenoid(url, fileName);
        }

        private string GetSessionId()
        {
            return (Driver as RemoteWebDriver).SessionId.ToString();
        }

        private void GetFileFromSelenoid(string url, string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, fileName);
            }
        }
    }
}
