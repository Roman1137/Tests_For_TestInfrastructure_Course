using System.Linq;
using FluentAssertions;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    [AllureNUnit]
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
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

        [Test]
        public void Complete_Should_Make_Element_Completed2()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.First().Complete();

            App.ToDoPage.FilterByCompleted();
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();
            App.ToDoPage.ToDoItems.First().IsCompleted().Should().BeTrue();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Complete_Should_Make_Element_Completed3()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.First().Complete();

            App.ToDoPage.FilterByCompleted();
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();
            App.ToDoPage.ToDoItems.First().IsCompleted().Should().BeTrue();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Complete_Should_Make_Element_Completed4()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.First().Complete();

            App.ToDoPage.FilterByCompleted();
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();
            App.ToDoPage.ToDoItems.First().IsCompleted().Should().BeTrue();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }
    }
}
