using System;
using System.Collections.Concurrent;
using System.Linq;
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
        private static ConcurrentDictionary<string, IWebDriver> DriverCollection = new ConcurrentDictionary<string, IWebDriver>();
        private static object locker = new object();

        public static IWebDriver Driver
        {
            get
            {
                return DriverCollection.First(pair => pair.Key == TestContext.CurrentContext.Test.ID).Value;
            }

            set
            {
                lock (locker)
                {
                    DriverCollection.TryAdd(TestContext.CurrentContext.Test.ID, value);
                }
            }
        }

        public WebDriverWait Wait { get; set; }

        public ToDoPage ToDoPage { get; set; }

        public Application()
        {
            Driver = GetDriver();
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

        private IWebDriver GetDriver()
        {
            IWebDriver driver = null;
            if (TestSettings.IsLocalBrowser)
            {
                driver = new ChromeDriver();
            }
            else
            {
                var options = new ChromeOptions();
                Driver = new RemoteWebDriver(TestSettings.SeleniumClusterUrl, options);
            }

            driver.Manage().Window.Maximize();
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            return driver;
        }
    }
}
