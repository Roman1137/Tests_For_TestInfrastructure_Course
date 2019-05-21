using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
                this.Wait();
                var todoWebElements = Driver.FindElements(By.CssSelector(".main .todo-list .todo"));
                return todoWebElements.Count > 0
                    ? todoWebElements.Select(el => new ToDoItem(App, el)).ToList()
                    : new List<ToDoItem>();
            }
        }

        public void Open()
        {
            this.Wait();
            Driver.Url = TestSettings.ToDoApplicationUrl.ToString();
        }

        public void DisableFilters()
        {
            this.Wait();
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/all']")).Click();
        }

        public void FilterByActive()
        {
            this.Wait();
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/active']")).Click();
        }

        public void FilterByCompleted()
        {
            this.Wait();
            Driver.FindElement(By.CssSelector("footer .filters a[href='#/completed']")).Click();
        }

        public string ItemsLeftCount()
        {
            this.Wait();
            return Driver.FindElement(By.CssSelector("footer .todo-count strong")).Text;
        }

        public void CreateItem(string name)
        {
            this.Wait();
            Driver.FindElement(By.CssSelector("header input.new-todo")).Click();
            Driver.FindElement(By.CssSelector("header input.new-todo")).SendKeys(name);
            Driver.FindElement(By.CssSelector("header input.new-todo")).SendKeys(Keys.Enter);
        }

        public void SetAllItemsAsCompleted()
        {
            this.Wait();
            Driver.FindElement(By.CssSelector(".main label[for='toggle-all']")).Click();
        }

        public void ClearCompleted()
        {
            this.Wait();
            Driver.FindElement(By.CssSelector("footer button.clear-completed")).Click();
        }

        private void Wait() => Thread.Sleep(1000);
    }
}
