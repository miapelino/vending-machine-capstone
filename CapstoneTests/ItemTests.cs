using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void ItemConstructor()
        {
            Item testItem = new Item("a1", "Chips", 1.50m);
            Assert.AreEqual("a1", testItem.Slot);
            Assert.AreEqual("Chips", testItem.Name);
            Assert.AreEqual(1.50m, testItem.Price);
            Assert.AreEqual(5, testItem.Quantity);


        }
        [TestMethod]
        public void FoodResponseTest()
        {
            Item testItem1 = new Item("A1", "Chips", 1.50m);
            Assert.AreEqual("Crunch Crunch, Yum!", testItem1.FoodResponse(testItem1.Slot));

            Item testItem2 = new Item("B1", "Chips", 1.50m);
            Assert.AreEqual("Munch Munch, Yum!", testItem2.FoodResponse(testItem2.Slot));
        }
    }
}
