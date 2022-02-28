using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;

namespace SeleniumTests
{
    class Ex13
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        protected WebDriverWait wait;


        [SetUp]

        public void SetupTest()
        {
            driver = new ChromeDriver();
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test()
        {
            OpenStorePage();
            AddItemsInVB();
            DeleteItemsFromVB();
        }
        public void AddItemsInVB()
        {
            for (int i = 0; i < 3; i++)
            {                
                Click(By.CssSelector("div#box-most-popular li:first-child"));
                int ItemBefore = Convert.ToInt32(driver.FindElement(By.ClassName("quantity")).Text);
                string ItemsAfter = Convert.ToString(ItemBefore + 1);
                if (IsElementPresent(By.Name("options[Size]")))
                {
                    driver.FindElement(By.Name("options[Size]")).Click();
                    new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByText("Small");
                }
                driver.FindElement(By.Name("add_cart_product")).Click();
                WaitUntilVisible(By.ClassName("quantity"), ItemsAfter);

                Click(By.XPath("//img[@alt='My Store']"));
            }            
            
        }

        public void DeleteItemsFromVB()
        {            
            driver.FindElement(By.LinkText("Checkout »")).Click();
            int CountInVB = driver.FindElements(By.CssSelector("td.item")).Count;
            for (int i = 0; i < CountInVB; i++)
            {
                int CountItems = driver.FindElements(By.CssSelector("td.item")).Count;
                driver.FindElement(By.Name("remove_cart_item")).Click();
                wait.Until((driver) => driver.FindElements(By.CssSelector("td.item")).Count() == CountItems - 1);                
            }
        }

        public void WaitUntilVisible(By locator, string text)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Message = "Element with text '" + text + "' was not visible in 10 seconds";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        public static Func<IWebDriver, IWebElement> Condition(By locator)
        {
            return (driver) =>
            {
                var element = driver.FindElements(locator).FirstOrDefault();
                return element != null && element.Displayed && element.Enabled ? element : null;
            };
        }

        protected void Click(By locator)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(Condition(locator)).Click();
        }
        public bool AreElementsPresent(By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }


        private void OpenStorePage()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/");
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
            driver = null;
        }
    }
}
