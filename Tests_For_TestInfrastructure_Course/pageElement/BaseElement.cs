using OpenQA.Selenium;
using Tests_For_TestInfrastructure_Course.app;

namespace Tests_For_TestInfrastructure_Course.pageElement
{
    public class BaseElement
    {
        protected Application App { get; set; }
        protected IWebDriver Driver { get; set; }

        public BaseElement(Application app)
        {
            this.App = app;
            this.Driver = Application.Driver;
        }
    }
}
