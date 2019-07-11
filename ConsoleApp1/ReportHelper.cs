using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    public class ReportHelper
    {
        private readonly string DataFilePath;
        private List<Data> DataList = new List<Data>();
        private List<string> DataLines = new List<string>();

        public ReportHelper(string dataFilePath)
        {
            this.DataFilePath = dataFilePath;
            LoadData();
            ParseData();
        }

        public void LoadData()
        {
            try
            {
                string[] lines = File.ReadAllLines(DataFilePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    //Skip the first line which is header
                    if (i > 0 && !string.IsNullOrEmpty(lines[i]))
                    {
                        DataLines.Add(lines[i]);
                    }
                }
            }
            catch (Exception exception)
            {
               throw new Exception("Error occured while loading data! \n\nReason: " + exception.ToString());
            }
        }

        public void ParseData()
        {
            foreach (var data in DataLines)
            {
                string[] dataParts = data.Split(',');
                if (dataParts != null && dataParts.Length > 1)
                {
                    DataList.Add(new Data()
                    {
                        Tenor = ParseTenor(dataParts[0].Trim()),
                        PortfolioID = dataParts[1].Trim(),
                        Value = Convert.ToDouble(dataParts[2].Trim())
                    });
                }
            }
        }

        private string ParseTenor(string rawTenor)
        {
            StringBuilder result = new StringBuilder();
            char[] chars = rawTenor.ToCharArray();
            if (chars != null)
            {
                int charsLength = chars.Length;
                for (int i = 0; i < charsLength; i++)
                {
                    char charToProcess = chars[i];
                    if (Char.IsDigit(charToProcess))
                    {
                        result.Append(charToProcess.ToString()).Append(" ");
                    }
                    else
                    {
                        string stringPart = PrepareStringPart(charToProcess, chars[i - 1]);
                        result.Append(stringPart);

                        if (charsLength > 2)
                        {
                            if (i <= charsLength - 3)
                            {
                                if (i == charsLength - 3)
                                {
                                    result.Append(" and ");
                                }
                                else
                                {
                                    result.Append(", ");
                                }
                            }
                        }
                    }
                }
            }

            return result.ToString();
        }

        private string PrepareStringPart(char charToProcess, char leadingIntValue)
        {
            string stringPart = string.Empty;
            bool isTimePartPlural = Convert.ToInt32(leadingIntValue) > 1;
            switch (charToProcess)
            {
                case 'd':
                    {
                        stringPart = isTimePartPlural ? "days" : "day";
                        break;
                    }
                case 'w':
                    {
                        stringPart = isTimePartPlural ? "weeks" : "week";
                        break;
                    }
                case 'm':
                    {
                        stringPart = isTimePartPlural ? "months" : "month";
                        break;
                    }
                case 'y':
                    {
                        stringPart = isTimePartPlural ? "years" : "year";
                        break;
                    }
                default:
                    break;
            }

            return stringPart;
        }

        public void DisplayDataByTenorAndPortfolioID()
        {
            DataList.Sort(new DataComparerByTenorThenPortfolioID());
            string dataToWrite = string.Empty;
            foreach (var item in DataList)
            {
                dataToWrite = string.Format("{0}\t{1}\t{2}", item.Tenor, item.PortfolioID, item.Value);
                Console.WriteLine(dataToWrite);
            }
        }

        public void DisplayDataByPortfolioIDAndTenor()
        {
            DataList.Sort(new DataComparerByPortfolioIDThenTenor());
            string dataToWrite = string.Empty;
            foreach (var item in DataList)
            {
                dataToWrite = string.Format("{0}\t{1}\t{2}", item.PortfolioID, item.Tenor, item.Value);
                Console.WriteLine(dataToWrite);
            }
        }

        public void WriteSortedData()
        {
            List<string> dataToWrite = new List<string>();
            DataList.Sort(new DataComparerByTenorThenPortfolioID());
            foreach (var item in DataList)
            {
                dataToWrite.Add(string.Format("{0}\t{1}\t{2}", item.Tenor, item.PortfolioID, item.Value));
            }
            File.WriteAllLines("../../3.txt", dataToWrite.ToArray());

            dataToWrite = new List<string>();

            DataList.Sort(new DataComparerByPortfolioIDThenTenor());
            foreach (var item in DataList)
            {
                dataToWrite.Add(string.Format("{0}\t{1}\t{2}", item.PortfolioID, item.Tenor, item.Value));
            }
            File.WriteAllLines("../../4.txt", dataToWrite.ToArray());
        }
    }
}
