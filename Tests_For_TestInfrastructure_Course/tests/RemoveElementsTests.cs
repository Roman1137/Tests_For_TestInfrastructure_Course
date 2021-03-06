﻿using System.Linq;
using FluentAssertions;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    [AllureNUnit]
    [TestFixture, Parallelizable(ParallelScope.All)]
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

        [Test]
        public void Delete_Should_Remove_Item2()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item3()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item4()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item5()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item6()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item7()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item8()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item9()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }

        [Test]
        public void Delete_Should_Remove_Item10()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.ToDoItems.First().Delete();
            App.ToDoPage.ToDoItems.Should().BeEmpty();
        }
    }
}
