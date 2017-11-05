using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class FileIOTests
    {
        [TestMethod]
        public void CreateInventoryListTest()
        {
            FileIO testObj = new FileIO();
            List<Item> answerList = new List<Item>();
            Item testItem1 = new Item("A1", "Potato Crisps", 3.05m);//check sample file for first and second "item"
            Item testItem2 = new Item("B1", "Moonpie", 1.80m);

            answerList.Add(testItem1);
            answerList.Add(testItem2);

            List<Item> testResults = testObj.CreateInventoryList();

            CollectionAssert.AreEqual(answerList, testResults);


        }
    }
}
