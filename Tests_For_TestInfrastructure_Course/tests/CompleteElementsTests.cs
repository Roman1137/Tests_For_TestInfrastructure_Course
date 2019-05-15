using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    public class CompleteElementsTests: BaseTest
    {
        [Test]
        public void Complete_Should_Make_Element_Completed()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Complete();
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();
            App.ToDoPage.ToDoItems.First().IsCompleted().Should().BeTrue();

            App.ToDoPage.FilterByCompleted();
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();
            App.ToDoPage.ToDoItems.First().IsCompleted().Should().BeTrue();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }
    }
}
