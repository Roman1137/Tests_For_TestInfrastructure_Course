using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Tests_For_TestInfrastructure_Course.app;

namespace Tests_For_TestInfrastructure_Course.pageElement
{
    public class ToDoItem : BaseElement
    {
        private readonly IWebElement _webElement;
        
        public ToDoItem(IWebElement webElement)
        {
            this._webElement = webElement;
        }

        public bool IsCompleted()
        {
            var classes = this._webElement.GetAttribute("class");
            var splitedClasses = classes.Split(" ");

            return splitedClasses.Contains("completed");
        }

        public string GetText()
        {
            return this._webElement.FindElement(By.CssSelector("label")).Text;
        }

        public void Complete()
        {
            this._webElement.FindElement(By.CssSelector(@"[type='checkbox'].toggle")).Click();
        }

        public void Delete()
        {
            var deleteButton =  this._webElement.FindElement(By.CssSelector("button.destroy"));
            ((IJavaScriptExecutor) Application.Driver).ExecuteScript("arguments[0].click();", deleteButton);
        }

        public void Edit(string newValue)
        {
            this.DoubleClick();
            this._webElement.FindElement(By.CssSelector("input[type='text'].edit")).SendKeys(newValue);
        }

        private void DoubleClick()
        {
            var elementToDoubleClick = this._webElement.FindElement(By.CssSelector("label"));
            new Actions(Application.Driver).DoubleClick(elementToDoubleClick);
        }
    }
}
