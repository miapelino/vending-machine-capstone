using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class CommandLineInterface
    {
        private VendingMachine ourVendingMachine;
        private bool isDone = false;

        public VendingMachine OurVendingMachine { get => ourVendingMachine; }
        public bool IsDone { get => isDone; set => isDone = value; }

        public CommandLineInterface(VendingMachine ourVendingMachine)
        {
            this.ourVendingMachine = ourVendingMachine;
        }
        public void Runner()
        {
            while (!isDone)
            {
                Console.WriteLine("You have $" + ourVendingMachine.Balance.ToString("F2") + " to spend");
                Console.WriteLine("Choose an option: 1. Display Items  2. Purchase Items  3. End Program");
                string userRunnerChoice = Console.ReadLine();
                string userRunnerChoiceLower = userRunnerChoice.ToLower();

                if (userRunnerChoiceLower.Contains("display items") || userRunnerChoice.Equals("1"))
                {
                    Console.WriteLine();
                    Console.WriteLine("Item Number".PadRight(20) + "Item Name".PadRight(20) + "Price".PadRight(20) + "Quantity");
                    Console.WriteLine("---------------------------------------------------------------------");
                    foreach (Item element in ourVendingMachine.Inventory)
                    {
                        Console.Write(element.Slot.PadRight(20));
                        Console.Write(element.Name.PadRight(20));
                        Console.Write(element.Price.ToString().PadRight(20));
                        Console.Write(element.Quantity.ToString().PadRight(20));
                        Console.WriteLine();
                    }

                    Console.WriteLine("Press 'enter' to continue");
                    Console.ReadLine();
                }


                else if (userRunnerChoiceLower.Contains("purchase items") || userRunnerChoice.Equals("2"))

                {
                    PurchaseMenu(userRunnerChoiceLower);

                }

                else if (userRunnerChoiceLower.Contains("end program") || userRunnerChoice.Equals("3"))
                {
                    FileIO myFile = new FileIO();
                    myFile.UpdateSalesReport(ourVendingMachine);
                    isDone = true;
                }

                else if (userRunnerChoiceLower.Contains("sales report") || userRunnerChoice.Equals("4"))
                {
                    FileIO myFile = new FileIO();
                    myFile.UpdateSalesReport(ourVendingMachine);
                    Console.WriteLine("Sales Report Generated.");
                    Runner();
                }

                else
                {
                    Console.WriteLine("Not a valid choice");
                    Console.WriteLine("Press 'enter' to continue");
                    Console.ReadLine();
                }
            }
        }
        public void PurchaseMenu(string selection)
        {
            Console.WriteLine("You have $" + ourVendingMachine.Balance.ToString("F2") + " to spend");
            Console.WriteLine("Choose an option: 1. Feed Money  2. Select Items  3. Finish Selection");
            string userSelection = Console.ReadLine();
            string userSelectionLower = userSelection.ToLower();

            if (userSelectionLower.Contains("feed money") || userSelection.Contains("1"))
            {
                FeedMoney();
            }

            else if (userSelectionLower.Contains("select items") || userSelection.Contains("2"))
            {
                SelectProduct();
            }

            else if (userSelectionLower.Contains("finish transaction") || userSelection.Contains("3"))
            {
                FinishTransaction();
            }


        }
        public void FeedMoney()
        {
            int userMoneyInt;
            bool feedMoneyMenu = true;
            decimal currentBalace = ourVendingMachine.Balance;

            while (feedMoneyMenu)
            {
                Console.WriteLine("Please insert individual bills ($1, $2, $5, $10, or $20)");
                string userMoneyString = Console.ReadLine();

                if (userMoneyString.Contains("$"))
                {
                    userMoneyString = userMoneyString.Substring(1);
                }


                if (userMoneyString.Equals("1") || userMoneyString.Equals("2") || userMoneyString.Equals("5") || userMoneyString.Equals("10") || userMoneyString.Equals("20"))
                {
                    userMoneyInt = Int32.Parse(userMoneyString);
                    ourVendingMachine.PlusBalance(userMoneyInt);
                    FileIO logWriter = new FileIO();
                    logWriter.CreateLogEntry("FEED MONEY", currentBalace, ourVendingMachine.Balance);

                }
                else
                {
                    Console.WriteLine("Incorrect dollar amount");
                    FeedMoney();
                }

                Console.WriteLine("Your current balance is: $" + ourVendingMachine.Balance.ToString("F2"));
                Console.WriteLine("Would you like to insert more money? (Y or N)");
                string userInsertMoneyChoice = Console.ReadLine();
                string userInsertMoneyChoiceLower = userInsertMoneyChoice.ToLower();

                if (userInsertMoneyChoiceLower.Contains("n"))
                {
                    feedMoneyMenu = false;
                    PurchaseMenu(userInsertMoneyChoiceLower);
                }
                else if (userInsertMoneyChoiceLower.Contains("y"))
                {
                    FeedMoney();
                }
                else
                {
                    Console.WriteLine("Invalid entry");
                    Console.WriteLine("Press enter to return to Main Menu");
                    Console.ReadLine();
                    Runner();
                }
            }
        }

        public void SelectProduct()
        {
            //Print the inventory for the user

            Console.WriteLine();
            Console.WriteLine("Item Number".PadRight(20) + "Item Name".PadRight(20) + "Price".PadRight(20) + "Quantity");
            Console.WriteLine("----------------------------------------------------------------------");
            foreach (Item element in ourVendingMachine.Inventory)
            {
                Console.Write(element.Slot.PadRight(20));
                Console.Write(element.Name.PadRight(20));
                Console.Write(element.Price.ToString().PadRight(20));
                Console.Write(element.Quantity.ToString().PadRight(20));
                Console.WriteLine();
            }

            //Ask for the users 
            Console.WriteLine();
            Console.Write("Enter a item number (Examples: A1, B2, D1): ");
            string slotChoice = Console.ReadLine().ToUpper();

            //If the slot number is invalid or the items are sold out
            if (!ourVendingMachine.CheckSlotExists(slotChoice))
            {
                Console.Write("Invalid item number, please check the menu and re-enter an item number");
                SelectProduct();
            }
            if (!ourVendingMachine.ItemExists(slotChoice))
            {
                Console.Write("SOLD OUT");
                Console.WriteLine();
                SelectProduct();
            }

            //If the items exists, check balance 
            if (ourVendingMachine.GetPrice(slotChoice) <= ourVendingMachine.Balance)
            {

                Console.WriteLine("You purchased " + ourVendingMachine.GetName(slotChoice));
                Console.WriteLine(ourVendingMachine.GetResponse(slotChoice));
                ourVendingMachine.UpdateInventory(slotChoice);//Decrement the quantity in inventory

                //Sort the current balance for log entry, then update the balance.
                decimal previousBalance = ourVendingMachine.Balance;
                ourVendingMachine.MinusBalance(ourVendingMachine.GetPrice(slotChoice));

                //Record the log entry
                FileIO logWriter = new FileIO();
                logWriter.CreateLogEntry($"PURCHASED {ourVendingMachine.GetName(slotChoice)} {slotChoice} ", previousBalance, ourVendingMachine.Balance);
                PurchaseMenu("2");
            }
            else
            {
                Console.WriteLine("Insufficient Funds");
                PurchaseMenu("2");
            }
        }
        public void FinishTransaction()
        {
            decimal currentBalance = ourVendingMachine.Balance;
            string[] changeMade = ourVendingMachine.MakeChange();//This sets the balance to zero too

            Console.WriteLine($"Thanks for your business! Your change is ${currentBalance}");
            foreach (string element in changeMade)
            {
                Console.WriteLine(element);
            }

            FileIO logWriter = new FileIO();
            logWriter.CreateLogEntry("MADE CHANGE", currentBalance, ourVendingMachine.Balance);
        }
    }
}
