using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SeleniumTests
{
    class Ex6
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        protected WebDriverWait wait;
        


        [SetUp]
        
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {
            OpenHomePage();
            Login();
            //Appearence            
            driver.FindElement(By.XPath("//li[@id='app-']/a/span[2]")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-template']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-logotype']/a/span")).Click();
            Assert.True(HeaderExist());
            //Catalog
            driver.FindElement(By.LinkText("Catalog")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-catalog']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-product_groups']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-option_groups']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-manufacturers']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-suppliers']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-delivery_statuses']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-sold_out_statuses']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-quantity_units']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-csv']/a/span")).Click();
            Assert.True(HeaderExist());
            //Countries
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='CSV Import/Export'])[1]/following::span[2]")).Click();
            Assert.True(HeaderExist());
            //Currencies
            driver.FindElement(By.LinkText("Currencies")).Click();
            Assert.True(HeaderExist());
            //Customers
            driver.FindElement(By.LinkText("Customers")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-customers']/a")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-csv']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-newsletter']/a/span")).Click();
            Assert.True(HeaderExist());
            //Geo zones
            driver.FindElement(By.LinkText("Geo Zones")).Click();
            Assert.True(HeaderExist());
            //Langueges
            driver.FindElement(By.LinkText("Languages")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-languages']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-storage_encoding']/a/span")).Click();
            Assert.True(HeaderExist());
            //Modules
            driver.FindElement(By.LinkText("Modules")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-jobs']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-customer']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-shipping']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-payment']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-order_total']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-order_success']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-order_action']/a/span")).Click();
            Assert.True(HeaderExist());
            //Orders
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Order Action'])[1]/following::span[2]")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-orders']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-order_statuses']/a/span")).Click();
            Assert.True(HeaderExist());
            //Pages
            driver.FindElement(By.LinkText("Pages")).Click();
            Assert.True(HeaderExist());
            //Reports
            driver.FindElement(By.LinkText("Reports")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-monthly_sales']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-most_sold_products']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-most_shopping_customers']/a/span")).Click();
            Assert.True(HeaderExist());
            //Settings
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Most Shopping Customers'])[1]/following::span[2]")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.LinkText("Store Info")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-defaults']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-general']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-listings']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-images']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-checkout']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-advanced']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-security']/a/span")).Click();
            Assert.True(HeaderExist());
            //Slides
            driver.FindElement(By.LinkText("Slides")).Click();
            Assert.True(HeaderExist());
            //Tax
            driver.FindElement(By.LinkText("Tax")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-tax_classes']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-tax_rates']/a/span")).Click();
            Assert.True(HeaderExist());
            //Translations
            driver.FindElement(By.LinkText("Translations")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-search']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-scan']/a/span")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-csv']/a/span")).Click();
            Assert.True(HeaderExist());
            //Users
            driver.FindElement(By.LinkText("Users")).Click();
            Assert.True(HeaderExist());
            //vQmods
            driver.FindElement(By.LinkText("vQmods")).Click();
            Assert.True(HeaderExist());
            driver.FindElement(By.XPath("//li[@id='doc-vqmods']/a/span")).Click();
            Assert.True(HeaderExist());
        }


        public By ExistHeader { get { return By.XPath("//td[@id='content']/h1"); } }

        public bool HeaderExist()
        {
            return IsElementPresent(ExistHeader);
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
