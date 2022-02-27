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
    public class Ex9
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
            OpenGeoZonePage();            
            GetCountryZoneList();
        }
               


        public void GetCountryZoneList()
        {            
            List<IWebElement> elementsGeoZone = driver.FindElements(By.XPath("//form[@name='geo_zones_form']/table//tr[@class='row']/td[3]/a")).ToList();
            for (int i = 0; i < elementsGeoZone.Count; i++)
            {
                var elementsGeoZ = driver.FindElements(By.XPath("//form[@name='geo_zones_form']/table//tr[@class='row']/td[3]/a")).ToList();
                List<IWebElement> elementsZone = driver.FindElements(By.XPath("//form[@name='geo_zones_form']/table//tr[@class='row']/td[4]")).ToList();
                if (elementsZone[i].GetAttribute("textContent") != "0")
                {
                    List<string> zones = new List<string>();
                    List<string> notSortZones = new List<string>();
                    elementsGeoZ[i].Click();
                    var elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr[not(@class)]/td[3]//option[@selected='selected']"));
                    foreach (var element in elements)
                    {
                        var ZoneName = element.GetAttribute("textContent");                        
                        zones.Add(ZoneName);
                        notSortZones.Add(ZoneName);
                    }                    
                    List<string> SortList = zones;
                    SortList.Sort();
                    Assert.AreEqual(notSortZones, SortList);

                    OpenGeoZonePage();
                }
            }
        }       

        private void OpenGeoZonePage()
        {
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Customers'])[1]/following::span[2]")).Click();
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
