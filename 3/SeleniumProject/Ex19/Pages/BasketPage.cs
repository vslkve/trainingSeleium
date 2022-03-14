using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class BasketPage
    {
        public By ItemsInTable { get { return By.CssSelector("td.item"); } }
        public By RemoveButton { get { return By.Name("remove_cart_item"); } }
    }
}
