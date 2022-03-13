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
    class Ex10_Chrome
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        protected WebDriverWait wait;


        [SetUp]

        public void SetupTest()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
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
            var colorMR = element.GetCssValue("color");
            string[] separators = { ",", "(", ")", "r","g", "b" };
            string[] ColorMRRGB = colorMR.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var ColorMRRGBTrim = ColorTrim(ColorMRRGB);
            //var colorMRR = element.GetCssValue("color").Substring(5, 3);
            //var colorMRG = element.GetCssValue("color").Substring(10, 3);
            //var colorMRB = element.GetCssValue("color").Substring(15, 3);
            double sizeMR = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2).Replace('.',','));            
            var decorMR = element.GetCssValue("text-decoration");
            element = driver.FindElement(By.ClassName("campaign-price"));
            var priceMC = element.GetAttribute("textContent");
            var colorMC = element.GetCssValue("color");
            string[] ColorMCRGB = colorMC.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var ColorMCRGBTrim = ColorTrim(ColorMCRGB);
            //var colorMCG = element.GetCssValue("color").Substring(10, 1); 
            //var colorMCB = element.GetCssValue("color").Substring(13, 1);
            double sizeMC = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2));            
            var weightMC = driver.FindElement(By.ClassName("campaign-price")).TagName;
            //акционная цена крупнее
            Assert.Greater(sizeMC, sizeMR);
            //обычная цена серая
            Assert.IsTrue(ColorMRRGBTrim[1] == ColorMRRGBTrim[2] && ColorMRRGBTrim[2] == ColorMRRGBTrim[3]);
            //Assert.IsTrue(colorMRR == colorMRG && colorMRG == colorMRB);
            //акционная красная
            //Assert.IsTrue(colorMCG == "0" && colorMCB == "0");           
            Assert.IsTrue(ColorMCRGBTrim[2] == "0" && ColorMCRGBTrim[3] == "0");

            driver.FindElement(By.CssSelector("div#box-campaigns a.link")).Click();

            var nameL = driver.FindElement(By.CssSelector("[itemprop=name]")).GetAttribute("textContent");
            element = driver.FindElement(By.ClassName("regular-price"));
            var priceLR = element.GetAttribute("textContent");
            var colorLR = element.GetCssValue("color");
            string[] ColorLRRGB = colorLR.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var ColorLRRGBTrim = ColorTrim(ColorLRRGB);
            //var colorLRR = element.GetCssValue("color").Substring(5, 3);
            //var colorLRG = element.GetCssValue("color").Substring(10, 3);
            //var colorLRB = element.GetCssValue("color").Substring(15, 3);
            double sizeLR = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2));
            var decorLR = element.GetCssValue("text-decoration");
            element = driver.FindElement(By.ClassName("campaign-price"));
            var priceLC = element.GetAttribute("textContent");
            var colorLC = element.GetCssValue("color");
            string[] ColorLCRGB = colorLC.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var ColorLCRGBTrim = ColorTrim(ColorLCRGB);
            //var colorLCG = element.GetCssValue("color").Substring(10, 1);
            //var colorLCB = element.GetCssValue("color").Substring(13, 1);
            double sizeLC = Convert.ToDouble(element.GetCssValue("font-size").Substring(0, element.GetCssValue("font-size").Length - 2));
            var weightLC = driver.FindElement(By.ClassName("campaign-price")).TagName; 
            //акционная цена крупнее
            Assert.Greater(sizeLC, sizeLR);
            //обычная цена серая
            Assert.IsTrue(ColorLRRGBTrim[1] == ColorLRRGBTrim[2] && ColorLRRGBTrim[2] == ColorLRRGBTrim[3]);
            //Assert.IsTrue(colorLRR == colorLRG && colorLRG == colorLRB);
            //акционная красная
            Assert.IsTrue(ColorLCRGBTrim[2] == "0" && ColorLCRGBTrim[3] == "0");
            //Assert.IsTrue(colorLCG == "0" && colorLCB == "0");

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

        public string[] ColorTrim(string[] color)
        {
            for (int i = 0; i < color.Length; i++)
                color[i] = color[i].Trim();
            return color;
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
