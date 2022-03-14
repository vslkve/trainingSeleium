using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace SeleniumTests
{
    class Ex19 : TestBase
    {
        [Test]
        public void Test()
        {
            app.OpenStorePage();
            app.AddItemsInVB();
            app.DeleteItemsFromVB();
        }
    }
}
