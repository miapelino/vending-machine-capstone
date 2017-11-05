using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes;

namespace Capstone.Classes
{
    public class FileIO
    {
        private Dictionary<string, decimal> salesTally = new Dictionary<string, decimal>();
        private decimal totalSales;

        public Dictionary<string, decimal> SalesTally { get => salesTally; set => salesTally = value; }

        public decimal TotalSales { get => totalSales; set => totalSales = value; }

        public FileIO()
        {

        }

        public List<Item> CreateInventoryList()
        {
            List<Item> inventory = new List<Item>();
            string directoryForImport = Environment.CurrentDirectory;
            string fileName = "vendingmachine.csv";//used for the real project
            //string testFileName = "testfile_vendingmachine.csv";//only for testing purposes

            string path = Path.Combine(directoryForImport, fileName);//used for real project
            //string path = Path.Combine(directoryForImport, testFileName);//Testing purposes only

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] itemParts = line.Split('|');
                        Item newItem = new Item(itemParts[0], itemParts[1], Convert.ToDecimal(itemParts[2]));
                        inventory.Add(newItem);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error reading file.");
                Console.Write(e.Message);
            }

            return inventory;
        }

        public void CreateLogEntry(string typeOfEntry, decimal previousBalance, decimal newBalance)
        {
            string directory = Environment.CurrentDirectory;
            string fileName = "Log_File.txt";

            string outputPath = Path.Combine(directory, fileName);
            try
            {
                using (StreamWriter sw = new StreamWriter(outputPath, true))
                {
                    var timeUtc = DateTime.UtcNow;
                    TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

                    sw.Write(easternTime.ToString() + " ");
                    sw.Write(typeOfEntry.PadRight(30));
                    sw.Write("$" + previousBalance.ToString("F2").PadRight(20));
                    sw.Write("$" + newBalance.ToString("F2"));
                    sw.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.Write("Error writing file.");
                Console.Write(e.Message);
            }
        }

        public void UpdateSalesReport(VendingMachine currentVendingMachine)
        {
            string directory = Environment.CurrentDirectory;
            string fileName = "SalesReport.txt";

            string inputPath = Path.Combine(directory, fileName);

            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    foreach (var item in currentVendingMachine.SalesReport)
                    {
                        if (item.Equals("TOTAL_SALES"))
                        {
                            sw.Write($"**TOTAL SALES** ${totalSales.ToString("F2")}");
                        }
                        else
                        {
                            sw.Write(item.Key + "|" + item.Value);
                        }
                        sw.WriteLine();
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write("Error reading file.");
                Console.Write(e.Message);
            }
        }
    }
}
