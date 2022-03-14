using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests
{
    public class MainPage
    {
        public void GoToMainPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost/litecart");
        }
        public By FirstProduct { get { return By.CssSelector("div#box-most-popular li:first-child"); } }
        public By ItemsCount { get { return By.ClassName("quantity"); } }
        public By HomePage { get { return By.XPath("//img[@alt='My Store']"); } }
    }
}
