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
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    public class Ex14
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
            OpenCloseNewWindow();
        }

        public void OpenCloseNewWindow()
        {
            driver.FindElement(By.LinkText("Afganistian")).Click();
            string mainCountryWindow = driver.CurrentWindowHandle; 
            int previousWinCount = driver.WindowHandles.Count; 
            IList<IWebElement> LinkList = driver.FindElements(By.CssSelector("a[target=_blank]"));
            for (int i = 0; i < LinkList.Count; i++)
            {
                string mainWindow = driver.CurrentWindowHandle;
                ICollection<string> oldWindows = driver.WindowHandles;

                LinkList[i].Click();
                string newWindow = wait.Until(AnyWindowOtherThan(oldWindows));
                driver.SwitchTo().Window(newWindow);
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("h1")));
                driver.Close();

                driver.SwitchTo().Window(mainWindow);
            }
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

        private void OpenCountryPage()
        {
            driver.FindElement(By.LinkText("Countries")).Click(); 

        }

        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/login.php");
        }

        private static Func<IWebDriver, string> AnyWindowOtherThan(ICollection<string> oldWindows)
        {
            return (driver) =>
            {
                List<string> newWindows = new List<string>(driver.WindowHandles);
                newWindows.RemoveAll(h => oldWindows.Contains(h));
                return newWindows.Count > 0 ? newWindows[0] : null;
            };
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
