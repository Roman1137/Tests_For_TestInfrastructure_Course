﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
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

        public static IWebDriver Driver
        {
            get
            {
                return DriverCollection.First(pair => pair.Key == TestContext.CurrentContext.Test.ID).Value;
            }

            set => DriverCollection.TryAdd(TestContext.CurrentContext.Test.ID, value);
        }

        public WebDriverWait Wait { get; set; }

        public ToDoPage ToDoPage { get; set; }

        public Application()
        {
            Driver = CreateDriver();
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

        private IWebDriver CreateDriver()
        {
            IWebDriver driver = null;
            switch (TestSettings.RunType)
            {
                case "DirectConnection":
                    {
                        switch (TestSettings.Browser)
                        {
                            case "Chrome":
                                var chromeOptions = new ChromeOptions();
                                if (Boolean.Parse(TestSettings.IsHeadlessMode))
                                {
                                    chromeOptions.AddArgument("--headless");
                                }
                                chromeOptions.AddArgument("--start-maximized");
                                driver = new ChromeDriver(Environment.CurrentDirectory, chromeOptions);
                                break;

                            case "Firefox":
                                var firefoxOptions = new FirefoxOptions();
                                if (Boolean.Parse(TestSettings.IsHeadlessMode))
                                {
                                    firefoxOptions.AddArgument("--headless");
                                }
                                firefoxOptions.UseLegacyImplementation = false;
                                firefoxOptions.AddArgument("--start-maximized");
                                driver = new FirefoxDriver(Environment.CurrentDirectory, firefoxOptions);
                                break;
                            default:
                                throw new Exception("Driver was not created");
                        }
                        break;
                    }
                case "SeleniumGrid":
                    {
                        switch (TestSettings.Browser)
                        {
                            case "Chrome":
                                var chromeOptions = new ChromeOptions();
                                chromeOptions.AddArgument("--start-maximized");
                                driver = new RemoteWebDriver(TestSettings.SeleniumClusterUrl, chromeOptions);
                                break;

                            case "Firefox":
                                var firefoxOptions = new FirefoxOptions();
                                firefoxOptions.AddArgument("--start-maximized");
                                driver = new RemoteWebDriver(TestSettings.SeleniumClusterUrl, firefoxOptions);
                                break;
                        }
                        break;
                    }
                case "Selenoid":
                    {
                        switch (TestSettings.Browser)
                        {
                            case "Chrome":
                                var chromeOptions = new ChromeOptions();
                                if (TestSettings.EnableVnc)
                                {
                                    chromeOptions.AddAdditionalCapability("enableVNC", true, true);
                                }
                                if (TestSettings.EnableVideoRecording)
                                {
                                    chromeOptions.AddAdditionalCapability("enableVideo", true, true);
                                }
                                if (TestSettings.EnableLogWritting)
                                {
                                    chromeOptions.AddAdditionalCapability("enableLog", true, true);
                                }
                                chromeOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.MethodName, true);
                                chromeOptions.AddArgument("--start-maximized");
                                driver = new RemoteWebDriver(TestSettings.SeleniumClusterUrl, chromeOptions);
                                break;

                            case "Firefox":
                                var firefoxOptions = new FirefoxOptions();
                                if (TestSettings.EnableVnc)
                                {
                                    firefoxOptions.AddAdditionalCapability("enableVNC", true, true);
                                }
                                if (TestSettings.EnableVideoRecording)
                                {
                                    firefoxOptions.AddAdditionalCapability("enableVideo", true, true);
                                }
                                if (TestSettings.EnableLogWritting)
                                {
                                    firefoxOptions.AddAdditionalCapability("enableLog", true, true);
                                }
                                firefoxOptions.AddArgument("--start-maximized");
                                driver = new RemoteWebDriver(TestSettings.SeleniumClusterUrl, firefoxOptions);
                                break;
                            case "Opera":
                                var operaOptions = new OperaOptions();
                                if (TestSettings.EnableVnc)
                                {
                                    operaOptions.AddAdditionalCapability("enableVNC", true, true);
                                }
                                if (TestSettings.EnableVideoRecording)
                                {
                                    operaOptions.AddAdditionalCapability("enableVideo", true, true);
                                }
                                if (TestSettings.EnableLogWritting)
                                {
                                    operaOptions.AddAdditionalCapability("enableLog", true, true);
                                }
                                operaOptions.AddArgument("--start-maximized");
                                driver = new RemoteWebDriver(TestSettings.SeleniumClusterUrl, operaOptions);
                                break;
                        }
                        break;
                    }
                default:
                    throw new Exception("Driver was not created");
            }
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestSettings.Timeout));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TestSettings.Timeout);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(TestSettings.Timeout);

            return driver;
        }
    }
}
