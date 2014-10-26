using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardproblems
{
    class Program
    {
        static void Main(string[] args)
        {
            Dekoder instance = new Dekoder();
            instance.ReadFromFile(string.Format("testset/test{0:D2}.SWE", 1));
            Solver s = new Solver();
            s.Solve(instance);

            Console.ReadLine();
        }
    }
}
