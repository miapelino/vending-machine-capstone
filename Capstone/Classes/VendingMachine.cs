using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<Item> inventory;
        private decimal balance = 0m;
        private Dictionary<string, decimal> salesReport = new Dictionary<string, decimal>();

        public List<Item> Inventory { get => inventory; }
        public decimal Balance { get => balance; }
        public Dictionary<string, decimal> SalesReport
        {
            get
            {
                return salesReport;
            }
            set
            {
                salesReport = value;
            }
        }

        //Create a vending maching that holds the current inventory (imported) and the lifetime
        //sales report (imported).

        public VendingMachine(List<Item> inventory)
        {
            this.inventory = inventory;
            balance = 0m;
            CreateSalesReport();
        }

        public string GetName(string slot)
        {
            string name = "";
            foreach (Item item in inventory)
            {
                if (item.Slot == slot)
                {
                    name = item.Name;
                }
            }
            return name;
        }

        public decimal GetPrice(string slot)
        {
            decimal price = 0m;
            foreach (Item item in inventory)
            {
                if (item.Slot == slot)
                {
                    price = item.Price;
                }
            }
            return price;
        }

        public int GetQuantity(string slot)
        {
            int quantity = 0;
            foreach (Item item in inventory)
            {
                if (item.Slot == slot)
                {
                    quantity = item.Quantity;
                }
            }
            return quantity;
        }

        public string GetResponse(string slot)
        {
            string response = "";
            foreach (Item item in inventory)
            {
                if (item.Slot == slot)
                {
                    response = item.FoodResponse(slot);
                }
            }
            return response;
        }

        public void UpdateInventory(string slot) 
        {
            foreach (Item item in inventory)
            {
                if (item.Slot == slot)
                {
                    item.Quantity--;
                    salesReport[item.Name]++;
                    salesReport["TOTAL_SALES"] += item.Price;
                    
                }
            }
        }

        public void CreateSalesReport()
        {
            string directory = Environment.CurrentDirectory;
            string fileName = "SalesReport.txt";

            string inputPath = Path.Combine(directory, fileName);

            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] getItems = line.Split('|');
                        salesReport.Add(getItems[0], decimal.Parse(getItems[1]));
                    }

                }
            }
            catch (Exception e)
            {
                Console.Write("Error reading file.");
                Console.Write(e.Message);
            }
        }

        public bool CheckSlotExists(string slot)
        {
            int exists = 0;
            foreach (Item item in inventory)
            {
                if (item.Slot == slot)
                {
                    exists++;
                }
            }
            if (exists == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ItemExists(string slot)
        {
            bool quantity = false;
            foreach (Item item in inventory)
            {
                if (item.Slot == slot && item.Quantity > 0)
                {
                    quantity = true;
                }
            }
            return quantity;
        }

        public void PlusBalance(int money)
        {
            balance = (decimal)balance + money;
        }

        public bool CheckFunds(decimal price)
        {
            return (price > balance) ? false : true;

        }

        public void MinusBalance(decimal price)
        {
                balance -= price;
        }

        public string[] MakeChange()
        {
            string[] noChange = new string[1] { "No change to provide. You spent all your money." };
            string[] makeChange = new string[3];

            decimal balanceToChange = balance*100;

            int quarters = 0;
            int dimes = 0;
            int nickels = 0;

            if (balance < 0)
            {
                return noChange;
            }
            else
            {
                //To get quarters:
                if (balanceToChange >= 25)
                {
                    quarters = (int)balanceToChange / 25;
                    balanceToChange %= 25;
                }

                makeChange[0] = $"{quarters} quarters";

                if (balanceToChange >= 10)
                {
                    dimes = (int)balanceToChange / 10;
                    balanceToChange %= 10;
                }

                makeChange[1] = $"{dimes} dimes";

                if (balanceToChange > 0)
                {
                    nickels = (int)balanceToChange / 5;
                }

                makeChange[2] = $"{nickels} nickels";

                balance = 0m;

                return makeChange;
            }
        }
    }
}
