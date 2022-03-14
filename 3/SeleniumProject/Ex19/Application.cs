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
    public class Application
    {
        public IWebDriver driver;
        private StringBuilder verificationErrors;
        protected WebDriverWait wait;

        public Application()
        {
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private MainPage mainPage;
        private ProductPage productPage;
        private BasketPage basketPage;

        public MainPage MainPage
        {
            get
            {
                if (mainPage == null)
                {
                    mainPage = new MainPage();
                }
                return mainPage;
            }
        }
        public ProductPage ProductPage
        {
            get
            {
                if (productPage == null)
                {
                    productPage = new ProductPage();
                }
                return productPage;
            }
        }
        public BasketPage BasketPage
        {
            get
            {
                if (basketPage == null)
                {
                    basketPage = new BasketPage();
                }
                return basketPage;
            }
        }
        public void AddItemsInVB()
        {
            for (int i = 0; i < 3; i++)
            {
                Click(MainPage.FirstProduct);
                int ItemBefore = Convert.ToInt32(driver.FindElement(MainPage.ItemsCount).Text);
                string ItemsAfter = Convert.ToString(ItemBefore + 1);
                if (IsElementPresent(ProductPage.Size))
                {
                    driver.FindElement(ProductPage.Size).Click();
                    new SelectElement(driver.FindElement(ProductPage.Size)).SelectByText("Small");
                }
                driver.FindElement(ProductPage.AddCartButton).Click();
                WaitUntilVisible(MainPage.ItemsCount, ItemsAfter);

                Click(MainPage.HomePage);
            }

        }

        public void DeleteItemsFromVB()
        {
            driver.FindElement(ProductPage.Cart).Click();
            int CountInVB = driver.FindElements(BasketPage.ItemsInTable).Count;
            for (int i = 0; i < CountInVB; i++)
            {
                int CountItems = driver.FindElements(BasketPage.ItemsInTable).Count;
                Click(BasketPage.RemoveButton);
                wait.Until((driver) => driver.FindElements(BasketPage.ItemsInTable).Count() == CountItems - 1);
            }
        }

        public void WaitUntilVisible(By locator, string text)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Message = "Element with text '" + text + "' was not visible in 10 seconds";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        public static Func<IWebDriver, IWebElement> Condition(By locator)
        {
            return (driver) =>
            {
                var element = driver.FindElements(locator).FirstOrDefault();
                return element != null && element.Displayed && element.Enabled ? element : null;
            };
        }

        protected void Click(By locator)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(Condition(locator)).Click();
        }
        public bool AreElementsPresent(By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }


        public void OpenStorePage()
        {
            MainPage.GoToMainPage(driver); 
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
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
