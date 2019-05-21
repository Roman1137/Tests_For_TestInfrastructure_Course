﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using Serilog;
using Tests_For_TestInfrastructure_Course.app;
using Tests_For_TestInfrastructure_Course.config;
using Tests_For_TestInfrastructure_Course.pageElement;

namespace Tests_For_TestInfrastructure_Course.pages
{
    public class ToDoPage : BasePage
    {
        public ToDoPage(Application app) : base(app) { }

        public IList<ToDoItem> ToDoItems
        {
            get
            {
                this.Wait();
                Log.Logger.Information("Getting ToDoItems");
                var todoWebElements = Driver.FindElements(By.CssSelector(".main .todo-list .todo"));
                return todoWebElements.Count > 0
                    ? todoWebElements.Select(el => new ToDoItem(App, el)).ToList()
                    : new List<ToDoItem>();
            }
        }

        public void Open()
        {
            this.Wait();
            Log.Logger.Information($"Browser is going to url: {TestSettings.ToDoApplicationUrl.ToString()}");
            Driver.Url = TestSettings.ToDoApplicationUrl.ToString();
        }

        public void DisableFilters()
        {
            this.Wait();
            Log.Logger.Information("Disabling filters");
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/all']")).Click();
        }

        public void FilterByActive()
        {
            this.Wait();
            Log.Logger.Information("Filtering by active");
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/active']")).Click();
        }

        public void FilterByCompleted()
        {
            this.Wait();
            Log.Logger.Information("Filtering by completed");
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/completed']")).Click();
        }

        public string ItemsLeftCount()
        {
            this.Wait();
            Log.Logger.Information("Getting count of left items");
            return Driver.FindElement(By.CssSelector("footer .todo-count strong")).Text;
        }

        public void CreateItem(string name)
        {
            this.Wait();
            Log.Logger.Information($"Creating item with name {name}");
            Driver.FindElement(By.CssSelector("header input.new-todo")).Click();
            Driver.FindElement(By.CssSelector("header input.new-todo")).SendKeys(name);
            Driver.FindElement(By.CssSelector("header input.new-todo")).SendKeys(Keys.Enter);
        }

        public void SetAllItemsAsCompleted()
        {
            this.Wait();
            Log.Logger.Information("Setting all items as completed");
            Driver.FindElement(By.CssSelector(".main label[for='toggle-all']")).Click();
        }

        public void ClearCompleted()
        {
            this.Wait();
            Log.Logger.Information("Clearing completed");
            Driver.FindElement(By.CssSelector("footer button.clear-completed")).Click();
        }

        private void Wait() => Thread.Sleep(1);
    }
}
