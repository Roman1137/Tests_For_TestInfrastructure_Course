using Serilog;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Tests_For_TestInfrastructure_Course.app;
using Tests_For_TestInfrastructure_Course.config;
using System.Net;

namespace Tests_For_TestInfrastructure_Course.pages
{
    public class FiledownloaderPage: BasePage
    {
        public FiledownloaderPage(Application app) : base(app) { }

        public void Open()
        {
            Log.Logger.Information($"Browser is going to url: {TestSettings.DownloadAppUrl.ToString()}");
            Driver.Url = TestSettings.DownloadAppUrl.ToString();
        }

        public void DownloadFirstItem()
        {
            var element = Driver.FindElements(By.CssSelector("a"))[1];
            
            var fileName = element.Text;
            //this.Wait();

            var url = $"http://localhost:4444/download/{GetSessionId()}/{fileName}";
            element.Click();
            GetFileFromSelenoid(url, fileName);
            // список файлов по сессии: http://localhost:4444/download/c300c6565b56b27c6787159dc251f09f/
            // я так попробовал только что - проверял список
            // дело в том, что там отображается файл, который ещё не полностью загружен
            // файл, что загружен нормлально, имеет расширение .zip, а тот, что ещё не полностью - zip.crdownload

            // http://localhost:4444/download/c300c6565b56b27c6787159dc251f09f/
            // список всех файлов СПИСОК ФАЙЛОВ по сессии
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
