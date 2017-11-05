using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    class MockItem
    {

        public string Slot
        {
            get
            {
                return Slot;
            }
        }

        public string Name
        {
            get
            {
                return Name;
            }
        }

        public decimal Price
        {
            get
            {
                return Price;
            }
        }

        public int Quantity
        {
            get
            {
                return Quantity;
            }
        }

        public MockItem(string Slot, string Name, decimal Price)
        {

        }
    }
}
