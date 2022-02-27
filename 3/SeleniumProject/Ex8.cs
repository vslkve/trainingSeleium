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
            GetCountryList();
            GetCountryZoneList();
        }
                
        public void GetCountryList()
        {
            List<string> countries = new List<string>();
            List<string> notSortCountries = new List<string>();
            List<IWebElement> elements = driver.FindElements(By.ClassName("row")).ToList();
            for (int i = 0; i < elements.Count; i++)
            {
                string CountryName = elements[i].FindElement(By.CssSelector("a")).GetAttribute("textContent");
                countries.Add(CountryName);
                notSortCountries.Add(CountryName);
            }            
            List<string> SortList = countries;
            SortList.Sort();
            Assert.AreEqual(notSortCountries, SortList);
        }

        public void GetCountryZoneList()
        {            
            List<IWebElement> elementsCountry = driver.FindElements(By.XPath("//form[@name='countries_form']/table//tr[@class='row']/td[5]/a")).ToList();            
            for (int i = 0; i < elementsCountry.Count; i++)
            {
                var elementsCo = driver.FindElements(By.XPath("//form[@name='countries_form']/table//tr[@class='row']/td[5]/a")).ToList(); 
                List<IWebElement> elementsZone = driver.FindElements(By.XPath("//*[@id='content']/form/table/tbody/tr[@class='row']/td[6]")).ToList();
                if (elementsZone[i].GetAttribute("textContent") != "0")
                {
                    List<string> zones = new List<string>();
                    List<string> notSortZones = new List<string>();
                    elementsCo[i].Click();

                    //List<IWebElement> elements = driver.FindElements(By.XPath("//table[@id='table-zones']//tr[not(@class)]/td[3]")).ToList();
                    //for (int j = 1; j <= elements.Count; j++)
                    //{
                    //    string ZoneName = elements[i].FindElement(By.XPath($"//*[@id='table - zones']/tbody/tr[{j}]/td[3]/input")).GetAttribute("value");
                    //    zones.Add(ZoneName);
                    //}

                    var elements = driver.FindElements(By.XPath($"//table[@id='table-zones']//tr[not(@class)]/td[3]"));
                    foreach (var element in elements)
                    {
                        var ZoneName = element.GetAttribute("value");
                        zones.Add(ZoneName);
                        notSortZones.Add(ZoneName);
                    }                    
                    List<string> SortList = zones;
                    SortList.Sort();
                    Assert.AreEqual(notSortZones, SortList);

                    OpenCountryPage();                    
                }
            }            
        }

        public void GetList()
        {

            List<IWebElement> elements = driver.FindElements(By.ClassName("row")).ToList();
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i].FindElement(By.CssSelector("a")).GetAttribute("textContent");
                //((IJavaScriptExecutor)driver).ExecuteScript($"return console.log(`{element}`)");

            }
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
