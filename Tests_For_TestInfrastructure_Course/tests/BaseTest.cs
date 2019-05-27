using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Allure.Commons;
using NUnit.Framework;
using Tests_For_TestInfrastructure_Course.app;

namespace Tests_For_TestInfrastructure_Course.tests
{
    public class BaseTest
    {
        protected Application App { get; set; }

        //[SetUp]
        public void SetUp2()
        {
            // TestContext.CurrentContext.Test.MethodName works only in [SetUp]
            var testName = TestContext.CurrentContext.Test.MethodName;
            Environment.SetEnvironmentVariable("testName", testName);

            this.App = new Application();
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            Environment.SetEnvironmentVariable(
                AllureConstants.ALLURE_CONFIG_ENV_VARIABLE,
                Path.Combine(Environment.CurrentDirectory, AllureConstants.CONFIG_FILENAME));
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
            //foreach (var toDoItem in this.App.ToDoPage.ToDoItems)
            //{
            //    toDoItem.Delete();
            //}
        }
    }
}
