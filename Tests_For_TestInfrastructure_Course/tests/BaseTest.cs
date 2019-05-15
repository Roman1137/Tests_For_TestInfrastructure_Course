using NUnit.Framework;
using Tests_For_TestInfrastructure_Course.app;

namespace Tests_For_TestInfrastructure_Course.tests
{
    public class BaseTest
    {
        protected Application App { get; set; }

        [SetUp]
        public void SetUp()
        {
            this.App = new Application();
        }

        [TearDown]
        public void TearDown()
        {
            this.App.Quit();
        }
    }
}
