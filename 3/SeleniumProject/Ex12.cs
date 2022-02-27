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
using System.IO;
using System.Reflection;

namespace SeleniumTests
{
    public class Ex12
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
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
            OpenCatalogPage();
            string ItemsName = GenerateItem();
            AddNewProduct(ItemsName);            
        }

        public void AddNewProduct(string ItemsName)
        {    
            driver.FindElement(By.LinkText("Add New Product")).Click();
            Thread.Sleep(1000);
            //General
            driver.FindElement(By.Name("status")).Click();
            driver.FindElement(By.Name("name[en]")).Click();
            driver.FindElement(By.Name("name[en]")).Clear();
            driver.FindElement(By.Name("name[en]")).SendKeys(ItemsName);
            driver.FindElement(By.Name("code")).Click();
            driver.FindElement(By.Name("code")).Clear();
            driver.FindElement(By.Name("code")).SendKeys("123123");
            driver.FindElement(By.Name("quantity")).Click();
            driver.FindElement(By.Name("quantity")).Clear();
            driver.FindElement(By.Name("quantity")).SendKeys("12");
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[7]/td/div/table/tbody/tr[4]/td/input")).Click();
            string namePath = "./crya.jpg";
            string itemPath = Path.GetFullPath(namePath);
            driver.FindElement(By.Name("new_images[]")).SendKeys(itemPath);
            driver.FindElement(By.Name("date_valid_from")).Click();
            driver.FindElement(By.Name("date_valid_from")).Clear();
            driver.FindElement(By.Name("date_valid_from")).SendKeys("2022-01-01");
            driver.FindElement(By.Name("date_valid_to")).Click();
            driver.FindElement(By.Name("date_valid_to")).Clear();
            driver.FindElement(By.Name("date_valid_to")).SendKeys("2023-12-31");
            //Information
            driver.FindElement(By.LinkText("Information")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Name("manufacturer_id")).Click();
            new SelectElement(driver.FindElement(By.Name("manufacturer_id"))).SelectByText("ACME Corp.");
            driver.FindElement(By.Name("keywords")).Click();
            driver.FindElement(By.Name("keywords")).Clear();
            driver.FindElement(By.Name("keywords")).SendKeys("duck");
            driver.FindElement(By.Name("short_description[en]")).Click();
            driver.FindElement(By.Name("short_description[en]")).Clear();
            driver.FindElement(By.Name("short_description[en]")).SendKeys("duck");
            driver.FindElement(By.Name("head_title[en]")).Click();
            driver.FindElement(By.Name("head_title[en]")).Clear();
            driver.FindElement(By.Name("head_title[en]")).SendKeys("duck");
            driver.FindElement(By.Name("meta_description[en]")).Click();
            driver.FindElement(By.Name("meta_description[en]")).Clear();
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("duck");
            //Prices
            driver.FindElement(By.LinkText("Prices")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Name("purchase_price")).Click();
            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys("15");
            driver.FindElement(By.Name("purchase_price_currency_code")).Click();
            new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code"))).SelectByText("US Dollars");
            driver.FindElement(By.Name("tax_class_id")).Click();
            driver.FindElement(By.Name("prices[USD]")).Click();
            driver.FindElement(By.Name("prices[USD]")).Clear();
            driver.FindElement(By.Name("prices[USD]")).SendKeys("15");
            driver.FindElement(By.Name("prices[EUR]")).Click();
            driver.FindElement(By.Name("prices[EUR]")).Clear();
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("12");
            driver.FindElement(By.Name("save")).Click();
            Thread.Sleep(1000);
            Assert.IsTrue(driver.FindElement(By.LinkText(ItemsName)).Displayed);
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
        public static string GenerateItem()
        {
            string ItemsName = GenerateRandomString();
            return ItemsName;
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

        private void OpenCatalogPage()
        {
            driver.FindElement(By.LinkText("Catalog")).Click();

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
