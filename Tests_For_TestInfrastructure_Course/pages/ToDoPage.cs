using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
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
                var todoWebElements = Driver.FindElements(By.CssSelector(".main .todo-list .todo"));
                return todoWebElements.Select(el => new ToDoItem(App, el)).ToList();
            }
        }

        public void Open()
        {
            Driver.Url = TestSettings.ToDoApplicationUrl.ToString();
        }

        public void DisableFilters()
        {
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/all']")).Click();
        }

        public void FilterByActive()
        {
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/active']")).Click();
        }

        public void FilterByCompleted()
        {
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/completed']")).Click();
        }

        public int ItemsLeftCount()
        {
            string countText = Driver.FindElement(By.CssSelector("footer .todo-count strong")).Text;
            return int.Parse(countText);
        }

        public void CreateItem(string name)
        {
            Driver.FindElement(By.CssSelector("header input.new-todo")).Click();
            Driver.FindElement(By.CssSelector("header input.new-todo")).SendKeys(name);
            Driver.FindElement(By.CssSelector("header input.new-todo")).SendKeys(Keys.Enter);
        }

        public void SetAllItemsAsCompleted()
        {
            Driver.FindElement(By.CssSelector(".main label[for='toggle-all']")).Click();
        }

        public void ClearCompleted()
        {
            Driver.FindElement(By.CssSelector("footer button.clear-completed")).Click();
        }
    }
}
