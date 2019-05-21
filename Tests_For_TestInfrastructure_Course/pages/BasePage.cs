using OpenQA.Selenium;
using Tests_For_TestInfrastructure_Course.app;

namespace Tests_For_TestInfrastructure_Course.pages
{
    public class BasePage
    {
        protected Application App { get; set; }
        protected IWebDriver Driver { get; set; }

        public BasePage(Application app)
        {
            this.App = app;
            this.Driver = Application.Driver;
        }
    }
}
