using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Item
    {
        private string slot = "";
        private string name = "";
        private decimal price = 0m;
        private int quantity = 5;

        public string Slot { get => slot; }
        public string Name { get => name; }
        public decimal Price { get => price; }
        public int Quantity { get => quantity; set => quantity = value; }

        public Item(string slot, string name, decimal price)
        {
            this.slot = slot;
            this.name = name;
            this.price = price;
        }

        public string FoodResponse(string slotNumber)
        {
            if (slotNumber.Contains('A'))
            {
                return "Crunch Crunch, Yum!";
            }
            else if (slotNumber.Contains('B'))
            {
                return "Munch Munch, Yum!";
            }
            else if (slotNumber.Contains('C'))
            {
                return "Glug Glug, Yum!";
            }
            else if (slotNumber.Contains('D'))
            {
                return "Chew Chew, Yum!";
            }
            return "";
        }

        public override bool Equals(object item)
        {
            Item something = (Item)item;
            return something.Slot == this.Slot && something.Name == this.Name;
        }
    }
}
