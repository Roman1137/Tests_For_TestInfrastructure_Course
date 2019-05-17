using NUnit.Framework;
using Tests_For_TestInfrastructure_Course.app;

namespace Tests_For_TestInfrastructure_Course.tests
{
    public class BaseTest
    {
        protected Application App { get; set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            this.App = new Application();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            this.App.Quit();
        }

        [TearDown]
        public void DeleteAllItems()
        {
            foreach (var toDoItem in this.App.ToDoPage.ToDoItems)
            {
                toDoItem.Delete();
            }
        }
    }
}
