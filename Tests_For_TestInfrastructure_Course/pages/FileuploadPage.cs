using Serilog;
using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Tests_For_TestInfrastructure_Course.app;
using Tests_For_TestInfrastructure_Course.config;

namespace Tests_For_TestInfrastructure_Course.pages
{
    public class FileuploadPage:BasePage
    {
        public FileuploadPage(Application app) : base(app) { }

        public void Open()
        {
            Log.Logger.Information($"Browser is going to url: {TestSettings.UploadAppUrl.ToString()}");
            Driver.Url = TestSettings.UploadAppUrl.ToString();
        }

        public FileuploadPage SelectPicture()
        {
            // передаем файл на удаленный барузер по W3C протоколу
            var allowsDetection = this.Driver as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }

            // был exception, но решил с помощью ссылки ниже
            // https://stackoverflow.com/questions/49215791/vs-code-c-sharp-system-notsupportedexception-no-data-is-available-for-encodin
            
            // https://stackoverflow.com/questions/50858209/system-notsupportedexception-no-data-is-available-for-encoding-1252/50875725
            // just added nuget package System.Text.Encoding.CodePages 
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var filePath = $"{Environment.CurrentDirectory}\\cat.jpg";
            Driver.FindElement(By.CssSelector("#file-upload")).SendKeys(filePath);
            return this;
        }

        public void Upload()
        {
            Driver.FindElement(By.CssSelector("#file-submit")).Click();
        }

        public void WaitUntilDownloaded()
        {
            App.Wait.Until(dr => dr.FindElement(By.CssSelector("h3")).Text.Contains("File Uploaded!"));
        }
    }
}
