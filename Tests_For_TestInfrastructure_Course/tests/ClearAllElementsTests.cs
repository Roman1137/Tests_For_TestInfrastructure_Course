﻿using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    public class ClearAllElementsTests: BaseTest
    {
        [Test]
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
    }
}