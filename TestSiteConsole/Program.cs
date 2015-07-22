using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSiteConsole.Delegate;

namespace TestSiteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DelegateTest dt = new DelegateTest();
            DelegateTest.Calculate add = new DelegateTest.Calculate(dt.AddNumbers);
            DelegateTest.Calculate mult = new DelegateTest.Calculate(dt.MultiplyNumbers);

            Console.WriteLine(add(4, 4));
            Console.WriteLine(mult(4, 4));
        }



    }
}
