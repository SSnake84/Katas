using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Wardrobe;

namespace Wardrobe.Tests
{
    [TestClass]
    public class WardrobeTests
    {

        List<Element> Elements = new List<Element> { 
            Element.Build(50), 
            Element.Build(75), 
            Element.Build(100), 
            Element.Build(120)
        };

        [TestMethod]
        public void Constructor_1()
        {
            var expected = 250;
            var wardrobe = new Wardrobe(expected);

            Assert.AreEqual(expected, wardrobe.TotalLength);
            Assert.AreEqual(0, wardrobe.Elements.Count);
        }

        [TestMethod]
        public void Constructor_2()
        {
            var expected = 250;
            var wardrobe = new Wardrobe(expected, Elements);

            Assert.AreEqual(expected, wardrobe.TotalLength);
            Assert.AreEqual(4, wardrobe.Elements.Count);
        }
        [TestMethod]
        public void GetResults_250()
        {
            var expected = 250;
            var wardrobe = new Wardrobe(expected, Elements);
            var result = wardrobe.GetResults();
            Assert.AreEqual(5, result.Count);
            // 5A , 3A+1C, 2A+2B, 1A+2C, 2B+1C
        }
        [TestMethod]
        public void GetResults_240()
        {
            var expected = 240;
            var wardrobe = new Wardrobe(expected, Elements);
            var result = wardrobe.GetResults();
            Assert.AreEqual(1, result.Count);
            // D+D
        }
    }
}