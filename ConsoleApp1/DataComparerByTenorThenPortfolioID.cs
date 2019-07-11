using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DataComparerByTenorThenPortfolioID : IComparer<Data>
    {
        public int Compare(Data data1, Data data2) {
            int result = data1.Tenor.CompareTo(data2.Tenor);
            if (result == 0)
            {
                result = data1.PortfolioID.CompareTo(data2.PortfolioID);
            }

            return result;
        }
    }
}
