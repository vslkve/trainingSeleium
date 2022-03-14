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
    public class TestBase
    {
        public Application app;

        [SetUp]
        public void SetupTest()
        {
            app = new Application();
        }             
                
        [TearDown]
        public void TeardownTest()
        {
            app.Stop();
        }
    }
}
