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

namespace SeleniumTests
{
    class Ex7
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
            Ducks();

        }
        
        public void Ducks()
        {
            List<IWebElement> elementsMenu = driver.FindElements(By.CssSelector("div.image-wrapper")).ToList();
            for(int i = 0; i < elementsMenu.Count; i++)
            {
                var element = elementsMenu[i].FindElements(By.CssSelector("div.sticker")).ToList();
                //((IJavaScriptExecutor)driver).ExecuteScript($"return console.log(`{i}`)");
                Assert.AreEqual(1, element.Count, $"failed i= {i}");
            }

            /*for (int i = 0; i < elementsMenu.Count; i++)
            {
                IList<IWebElement> sticker = (IList<IWebElement>)((IJavaScriptExecutor)driver).ExecuteScript("return $('.sticker')");
                Assert.AreEqual(1, sticker.Count);
            }*/
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
