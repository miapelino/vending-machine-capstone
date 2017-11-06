using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void TestGetName()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);

            string result = vendMach.GetName(testItem1.Slot);
            Assert.AreEqual("Potato Crisps", result);

            result = vendMach.GetName(testItem2.Slot);
            Assert.AreEqual("Moonpie", result);
        }

        [TestMethod]
        public void TestGetQuantity()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);

            int result = vendMach.GetQuantity(testItem1.Slot);
            Assert.AreEqual(5, result);

            string slot = testItem2.Slot;
            vendMach.UpdateInventory(slot);
            vendMach.UpdateInventory(slot);
            vendMach.UpdateInventory(slot);
            vendMach.UpdateInventory(slot);
            vendMach.UpdateInventory(slot);
            result = vendMach.GetQuantity(testItem2.Slot);
            Assert.AreEqual(0, result);
        }

        //WAITING ON PULL
        //[TestMethod]
        //public void TestGetResponse()
        //{

        //}

        [TestMethod]
        public void TestUpdateInventory()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);
            string slot = testItem1.Slot;

            vendMach.UpdateInventory(slot);
            int result = testItem1.Quantity;

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void TestCheckSlotExists()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);
            //string slot = testItem1.Slot;

            bool result = vendMach.CheckSlotExists(testItem1.Slot);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestItemExists()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            answerList.Add(testItem1);
            VendingMachine vendMach = new VendingMachine(answerList);

            bool result = vendMach.ItemExists(testItem1.Slot);

            Assert.AreEqual(true, result);

            result = vendMach.ItemExists("c4");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestPlusBalance()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            answerList.Add(testItem1);
            VendingMachine vendMach = new VendingMachine(answerList);

            vendMach.PlusBalance(5);
            decimal result = vendMach.Balance;

            Assert.AreEqual(5, result);

            vendMach.PlusBalance(10);
            result = vendMach.Balance;

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void CheckFunds()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);

            vendMach.PlusBalance(5);
            vendMach.MinusBalance(testItem1.Price);

            bool result = vendMach.CheckFunds(testItem2.Price);

            Assert.AreEqual(true, result);

            result = vendMach.CheckFunds(testItem1.Price);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestMinus()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);

            vendMach.PlusBalance(5);
            vendMach.MinusBalance(testItem1.Price);
            decimal result = vendMach.Balance;

            Assert.AreEqual((decimal)1.95, result);
        }

        [TestMethod]
        public void TestCheckFunds()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);

            vendMach.PlusBalance(5);
            bool result = vendMach.CheckFunds(testItem1.Price);
            Assert.AreEqual(true, result);

            vendMach.MinusBalance(testItem1.Price);
            result = vendMach.CheckFunds(testItem2.Price);
            Assert.AreEqual(true, result);

            vendMach.MinusBalance(testItem1.Price);
            result = vendMach.CheckFunds(testItem1.Price);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestMakeChange()
        {
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);
            answerList.Add(testItem1);
            answerList.Add(testItem2);
            VendingMachine vendMach = new VendingMachine(answerList);

            vendMach.PlusBalance(5);
            string[] result = vendMach.MakeChange();
            string[] expected = new string[3] { "20 quarters", "0 dimes", "0 nickels" };
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
