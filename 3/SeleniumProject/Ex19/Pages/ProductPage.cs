using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class ProductPage
    {
        public By Size { get { return By.Name("options[Size]"); } }
        public By AddCartButton { get { return By.Name("add_cart_product"); } }
        public By Cart { get { return By.LinkText("Checkout »"); } }
    }
}
