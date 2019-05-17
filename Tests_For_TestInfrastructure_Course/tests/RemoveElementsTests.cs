using System.Linq;
using FluentAssertions;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    [AllureNUnit]
    public class RemoveElementsTests: BaseTest
    {
        [Test]
        public void Delete_Should_Remove_Item()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }
    }
}
