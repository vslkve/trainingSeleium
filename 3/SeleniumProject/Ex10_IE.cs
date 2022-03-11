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
using OpenQA.Selenium.IE;

namespace SeleniumTests
{
    class Ex10_IE
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        protected WebDriverWait wait;


        [SetUp]

        public void SetupTest()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.RequireWindowFocus = true;
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Dismiss;
            driver = new InternetExplorerDriver(options);
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test()
        {
            OpenStorePage();
            CampaignDuck();

        }

        public void CampaignDuck()
        {
            IWebElement element;

            var nameM = driver.FindElement(By.CssSelector("div#box-campaigns div.name")).GetAttribute("textContent");

            element = driver.FindElement(By.ClassName("regular-price"));
            var priceMR = element.GetAttribute("textContent");
            var colorMRR = element.GetCssValue("color").Substring(5, 3);
            var colorMRG = element.GetCssValue("color").Substring(10, 3);
            var colorMRB = element.GetCssValue("color").Substring(15, 3);
            double sizeMR = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2).Replace('.', ','));
            var decorMR = element.GetCssValue("text-decoration");
            element = driver.FindElement(By.ClassName("campaign-price"));
            var priceMC = element.GetAttribute("textContent");
            var colorMCG = element.GetCssValue("color").Substring(10, 1);
            var colorMCB = element.GetCssValue("color").Substring(13, 1);
            double sizeMC = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2));
            var weightMC = driver.FindElement(By.ClassName("campaign-price")).TagName;
            //акционная цена крупнее
            Assert.Greater(sizeMC, sizeMR);
            //обычная цена серая
            Assert.IsTrue(colorMRR == colorMRG && colorMRG == colorMRB);
            //акционная красная
            Assert.IsTrue(colorMCG == "0" && colorMCB == "0");

            driver.FindElement(By.CssSelector("div#box-campaigns a.link")).Click();

            var nameL = driver.FindElement(By.CssSelector("[itemprop=name]")).GetAttribute("textContent");
            element = driver.FindElement(By.ClassName("regular-price"));
            var priceLR = element.GetAttribute("textContent");
            var colorLRR = element.GetCssValue("color").Substring(5, 3);
            var colorLRG = element.GetCssValue("color").Substring(10, 3);
            var colorLRB = element.GetCssValue("color").Substring(15, 3);
            double sizeLR = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2));
            var decorLR = element.GetCssValue("text-decoration");
            element = driver.FindElement(By.ClassName("campaign-price"));
            var priceLC = element.GetAttribute("textContent");
            var colorLCG = element.GetCssValue("color").Substring(10, 1);
            var colorLCB = element.GetCssValue("color").Substring(13, 1);
            double sizeLC = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2));
            var weightLC = driver.FindElement(By.ClassName("campaign-price")).TagName;
            //акционная цена крупнее
            Assert.Greater(sizeLC, sizeLR);
            //обычная цена серая
            Assert.IsTrue(colorLRR == colorLRG && colorLRG == colorLRB);
            //акционная красная
            Assert.IsTrue(colorLCG == "0" && colorLCB == "0");

            //Имя
            Assert.AreEqual(nameM, nameL);

            //совпадают цены
            Assert.IsTrue(priceMR == priceLR);
            Assert.IsTrue(priceMC == priceLC);

            //обычная цена зачёркнутая
            Assert.IsTrue(decorMR.Contains("line-through"));
            Assert.IsTrue(decorLR.Contains("line-through"));

            //акционная жирная
            Assert.AreEqual(weightMC, "strong");
            Assert.AreEqual(weightLC, "strong");

        }

        public bool AreElementsPresent(By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }


        private void OpenStorePage()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/"); Thread.Sleep(1000);
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
            //driver.Quit();
            // driver = null;
        }
    }
}
