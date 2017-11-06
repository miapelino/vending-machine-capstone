using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {

            FileIO fileIO = new FileIO();


            VendingMachine ourVendMach = new VendingMachine(fileIO.CreateInventoryList());

            CommandLineInterface ourCLI = new CommandLineInterface(ourVendMach);

            ourCLI.Runner();
        }
    }
}
