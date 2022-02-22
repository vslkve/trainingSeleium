using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Collections.Generic;
using System.Linq;


namespace SeleniumTests
{
    class Ex6
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        protected WebDriverWait wait;
        

        [SetUp]
        
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            

        }

        [Test]
        public void Test()
        {
            OpenHomePage();
            Login();
            Menu();          

        }
        
        public void Menu()
        {
            List<IWebElement> elementsMenu = driver.FindElements(By.Id("app-")).ToList();
            for (int i=0; i < elementsMenu.Count; i++)
            {
                var elements = driver.FindElement(By.XPath($"//ul[@id='box-apps-menu']/li[{i}+1]"));
                elements.Click();
                Assert.True(AreElementsPresent(By.XPath("//td[@id='content']/h1")));
                if(AreElementsPresent(By.ClassName("docs")))
                {
                    var elementUnderMenu = driver.FindElements(By.XPath($"//ul[@id='box-apps-menu']/li[{i}+1]/ul/li")).ToList();
                    for (int j = 0; j < elementUnderMenu.Count; j++)
                    {
                        var elementUnder = driver.FindElement(By.XPath($"//ul[@id='box-apps-menu']/li[{i}+1]/ul/li[{j}+1]"));
                        elementUnder.Click();
                        //Thread.Sleep(100);
                        Assert.True(AreElementsPresent(By.XPath("//td[@id='content']/h1")));
                    }
                }
            }
        }

        

        public By ExistHeader { get { return By.XPath("//td[@id='content']/h1"); } }

        public bool HeaderExist()
        {
            return IsElementPresent(ExistHeader);
        }

        public bool AreElementsPresent(By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

       

        private void Logout()
        {
            driver.FindElement(By.XPath("//td[@id='sidebar']/div[2]/a[5]/i")).Click();
        }

        
        private void Login()
        {
            driver.FindElement(By.Name("username")).Click();
            driver.FindElement(By.Name("username")).Clear();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/login.php");
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
