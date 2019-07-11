using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dataFilePath = "../../Data.txt";
            DisplayMenu();
            string selectedMenuItem = Console.ReadLine();
            if (!string.IsNullOrEmpty(selectedMenuItem))
            {
                switch (selectedMenuItem)
                {
                    case "3":
                        {
                            new ReportHelper(dataFilePath).DisplayDataByTenorAndPortfolioID();
                            break;
                        }
                    case "4":
                        {
                            new ReportHelper(dataFilePath).DisplayDataByPortfolioIDAndTenor();
                            break;
                        }
                    case "5":
                        {
                            new ReportHelper(dataFilePath).WriteSortedData();
                            break;
                        }
                    default:
                        {
                            //Resource can be used.
                            throw new Exception("Invalid menu item selection!");  
                        }
                }
            }

            Console.ReadLine();
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("3- Output the data sorted by Tenor and PortfolioID");
            Console.WriteLine("4- Output the data sorted by PortfolioID and Tenor");
            Console.WriteLine("5- Write the results from question 3 and 4 to flat files called 3.txt and 4.txt");
        }
    }
}
