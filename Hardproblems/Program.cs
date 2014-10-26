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
            instance.ReadFromFile(string.Format("testset/test{0:D2}.SWE",2));
            Solver s = new Solver();
            Dictionary<char, string> solution =  s.Solve(instance);

            if (solution != null)
            {
                Console.WriteLine("YES");
                foreach (KeyValuePair<char, string> solutionPair in solution)
	            {
                    Console.WriteLine(solutionPair.Key + ":" + solutionPair.Value);
	            }
            }
            else
            {
                Console.WriteLine("NO");
            }

            Console.ReadLine();
        }
    }
}
