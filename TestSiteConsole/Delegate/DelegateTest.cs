using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSiteConsole.Delegate
{
    public class DelegateTest
    {
        public delegate int Calculate(int a, int b);

        public  int AddNumbers(int a, int b)
        {
            return a + b;
        }

        public  int MultiplyNumbers(int a, int b)
        {
            return a * b;
        }
    }
}
