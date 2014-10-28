using System;
using System.Collections.Generic;

namespace Hardproblems
{
    static class Program
    {
        static void Main(string[] args)
        {
            Dekoder instance = new Dekoder();
            instance.ReadFromStdIn();
            Solver s = new Solver();
            Dictionary<char, string> solution =  s.Solve(instance);

            if (solution != null)
            {
                //Console.WriteLine("YES");
                foreach (KeyValuePair<char, string> solutionPair in solution)
	            {
                    Console.WriteLine(solutionPair.Key + ":" + solutionPair.Value);
	            }
            }
            else
            {
                Console.WriteLine("NO");
            }

            //Console.ReadLine();
        }
    }
}
