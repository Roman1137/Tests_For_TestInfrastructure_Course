using System;
using System.IO;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Tests_For_TestInfrastructure_Course.app;

namespace Tests_For_TestInfrastructure_Course.tests
{
    public class BaseTest
    {
        protected Application App { get; set; }

        [OneTimeSetUp]
        public void Configure()
        {
            Environment.SetEnvironmentVariable(
                AllureConstants.ALLURE_CONFIG_ENV_VARIABLE,
                Path.Combine(Environment.CurrentDirectory, AllureConstants.CONFIG_FILENAME));
        }

        [SetUp]
        public void SetUp()
        {
            this.App = new Application();
        }

        [TearDown]
        public void TearDown()
        {
            this.App.Quit();
            //Application.Driver.Close();
            //Application.Driver.Quit();
            //Application.Driver.Quit();
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
