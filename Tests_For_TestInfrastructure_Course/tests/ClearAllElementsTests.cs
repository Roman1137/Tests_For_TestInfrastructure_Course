using System.Linq;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using Serilog;

namespace Tests_For_TestInfrastructure_Course.tests
{
    // У меня получилось запустить тесты с ParallelScope.Fixtures.
    // Думаю, что разница между ParallelScope.All и ParallelScope.Fixtures состоит в том, что с использованием
    // ParallelScope.Fixtures - мы будем иметь ровно столько потоков, сколько фикстур у нас есть. В каждом потоке будут
    // запускаться по очереди тесты из фикстур. Если мы используем ParallelScope.All - то будет выделяться поток на каждый тест.
    // Ограничить количество поток можно с в обеих случаях помощью [assembly:LevelOfParallelism(3)].

    //Думаю, что если запускать для каждого теста свой браузер, то ParallelScope.Fixtures не имеет смысла.
    //Лучше использовать ParallelScope.All - так все потоки будут равномерно нагружены(например может быть ситуция,
    //когда в одной фикстуре 1 тест, а в другой 50 и если использовать ParallelScope.Fixtures - то поток с 1 тестом будет простаивать)
    [TestFixture]
    [AllureNUnit]
    //[TestFixture, Parallelizable(ParallelScope.Fixtures)] // тоже работает. Читай разницу между ParallelScope.All и ParallelScope.Fixtures выше
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class ClearAllElementsTests: BaseTest
    {
        [Test]
        [AllureTag("NUnit","Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureFeature("Core")]
        public void ClearCompleted_Should_Remove_All_Completed_Elements()
        {
            App.ToDoPage.Open();
            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.CreateItem("test3");

            App.ToDoPage.SetAllItemsAsCompleted();

            App.ToDoPage.ClearCompleted();

            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        [AllureTag("NUnit","Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureFeature("Core")]
        public void ClearCompleted_Should_Remove_All_Completed_Elements2()
        {
            App.ToDoPage.Open();
            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.CreateItem("test3");

            App.ToDoPage.SetAllItemsAsCompleted();

            App.ToDoPage.ClearCompleted();

            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void ClearCompleted_Should_Not_Remove_Active_Elements()
        {
            App.ToDoPage.Open();
            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.CreateItem("test3");

            App.ToDoPage.SetAllItemsAsCompleted();
            App.ToDoPage.CreateItem("test4");

            App.ToDoPage.ClearCompleted();

            App.ToDoPage.ToDoItems.Count.Should().Be(1);
            App.ToDoPage.ToDoItems.First().GetText().Should().Be("test4");
        }

        [Test]
        public void ClearCompleted_Should_Not_Remove_Active_Elements2()
        {
            App.ToDoPage.Open();
            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.CreateItem("test3");

            App.ToDoPage.SetAllItemsAsCompleted();
            App.ToDoPage.CreateItem("test4");

            App.ToDoPage.ClearCompleted();

            App.ToDoPage.ToDoItems.Count.Should().Be(1);
            App.ToDoPage.ToDoItems.First().GetText().Should().Be("test4");
        }
    }
}
