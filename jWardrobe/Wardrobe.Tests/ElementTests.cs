using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Wardrobe;

namespace Wardrobe.Tests
{
    [TestClass]
    public class ElementTests
    {
        [TestMethod]
        public void Constructor_1() 
        {
            var el = Element.Build(25);
            Assert.AreEqual("25 cm", el.ToString());
        }
    }
}
