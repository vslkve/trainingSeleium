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
    class Ex11
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
            string email = GenerateEmail();
            Registration(email);
            Logout();
            Login(email);
            Logout();
        }

        public void Registration(string email)
        {
            driver.FindElement(By.LinkText("New customers click here")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys("Tolya");
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys("Moroz");
            driver.FindElement(By.Name("address1")).Clear();
            driver.FindElement(By.Name("address1")).SendKeys("address");
            driver.FindElement(By.Name("postcode")).Clear();
            driver.FindElement(By.Name("postcode")).SendKeys("12345");
            driver.FindElement(By.Name("city")).Clear();
            driver.FindElement(By.Name("city")).SendKeys("NY");
            driver.FindElement(By.CssSelector("[role=combobox]")).Click();
            driver.FindElement(By.CssSelector("input[type=search]")).Clear();
            driver.FindElement(By.CssSelector("input[type=search]")).SendKeys("United States");
            driver.FindElement(By.CssSelector("input[type=search]")).SendKeys(Keys.Enter);
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).Clear();
            driver.FindElement(By.Name("phone")).SendKeys("1234567890");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("password");
            driver.FindElement(By.Name("confirmed_password")).Clear();
            driver.FindElement(By.Name("confirmed_password")).SendKeys("password");
            driver.FindElement(By.Name("create_account")).Click();
        }

        public void Login(string email)
        {
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("password");
            driver.FindElement(By.Name("login")).Click();
        }
        public void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString()
        {
            int l = Convert.ToInt32(rnd.NextDouble() * 9);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(97 + Convert.ToInt32(rnd.NextDouble() * 25)));
            }
            return builder.ToString();
        }
        public static string GenerateEmail()
        {
            string email = GenerateRandomString() + "@gmail.com";
            return email;
        }

        private void OpenStorePage()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/");
        }
                
        public bool AreElementsPresent(By locator)
        {
            return driver.FindElements(locator).Count > 0;
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
