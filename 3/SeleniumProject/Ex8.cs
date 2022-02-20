using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class Ex8 
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        protected WebDriverWait wait;


        [SetUp]

        public void SetupTest()
        {
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


        }

        [Test]
        public void Test()
        {
            OpenHomePage();
            Login();
            OpenCountryPage();
            
            List<CountryList> notSortList = GetCountryList();
            List<CountryList> SortList = GetCountryList();

            var SortListNew = from u in SortList
                              orderby u descending
                              select u;
            Assert.AreEqual(notSortList, SortList);
        }
         
        

        public List<CountryList> GetCountryList()
        {
            List<CountryList> countries = new List<CountryList>();
            ICollection<IWebElement> elements = driver.FindElements(By.ClassName("row")).ToList();
            foreach (IWebElement element in elements)
            {
                countries.Add(new CountryList(element.GetAttribute("textContent"))
                {
                    CountryName = element.FindElement(By.CssSelector("a")).GetAttribute("textContent")
                });
            }
            return countries;
        }

        private void OpenCountryPage()
        {
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Catalog'])[1]/following::span[2]")).Click();
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
