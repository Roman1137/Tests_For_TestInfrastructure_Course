using System;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Serilog;
using Serilog.Events;
using Tests_For_TestInfrastructure_Course.config;
using Tests_For_TestInfrastructure_Course.pages;

namespace Tests_For_TestInfrastructure_Course.app
{
    public class Application
    {
        private static ThreadLocal<IWebDriver> _Driver { get; set; }

        public static IWebDriver Driver => _Driver.Value;

        public WebDriverWait Wait { get; set; }

        public ToDoPage ToDoPage { get; set; }

        public Application()
        {
            InitializeDriver();
            InitializeLogger();

            this.ToDoPage = new ToDoPage(this);
        }

        public void Quit()
        {
            bool isSomeTestFailed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed;
            if (isSomeTestFailed)
            {
                this.TakeScreenShot();
            }

            Driver.Close();
            Driver.Quit();
            Driver.Dispose();
        }

        private void TakeScreenShot()
        {
            var fileName = TestContext.CurrentContext.TestDirectory + "\\" +
                           DateTime.Now.ToString("yy-MM-dd-HH-mm-ss-FFF") + "-" + GetType().Name + "-" +
                           TestContext.CurrentContext.Test.FullName + "." + ScreenshotImageFormat.Jpeg;
            try
            {
                ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
                TestContext.AddTestAttachment(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Debug)
                .WriteTo.Console()
                .CreateLogger();
        }

        private void InitializeDriver()
        {
            if (TestSettings.IsLocalBrowser)
            {
                _Driver = new ThreadLocal<IWebDriver>(() => new ChromeDriver());
            }
            else
            {
                var options = new ChromeOptions();
                _Driver = new ThreadLocal<IWebDriver>(() => new RemoteWebDriver(TestSettings.SeleniumClusterUrl, options));
            }

            Driver.Manage().Window.Maximize();
            this.Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }
    }
}
