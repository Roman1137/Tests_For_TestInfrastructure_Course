﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    public class CreateElementsTests: BaseTest
    {
        [Test]
        public void Create_Should_Create_One_Item()
        {
            App.ToDoPage.Open();

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.FilterByCompleted();
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();

            App.ToDoPage.FilterByActive();
            App.ToDoPage.ToDoItems.Should().NotBeEmpty();
        }

        [Test]
        public void Create_Should_Increase_Items_Left_Number()
        {
            App.ToDoPage.Open();
            App.ToDoPage.ItemsLeftCount().Should().Be("");

            App.ToDoPage.CreateItem("test1");
            App.ToDoPage.ItemsLeftCount().Should().Be("1");

            App.ToDoPage.CreateItem("test2");
            App.ToDoPage.ItemsLeftCount().Should().Be("2");
        }
    }
}
