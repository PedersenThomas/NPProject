using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hardproblems
{
    static class Program
    {
        static void Main(string[] args)
        {
            string filename = Console.ReadLine();
            Dekoder instance = new Dekoder();
            instance.ReadFromFile(filename);
            Solver s = new Solver();
            Dictionary<char, string> solution = s.Solve(instance);

            if (solution != null)
            {
                string solutionFile = filename.Replace(".SWE", ".SOL");
                List<char> gamma = instance.RefereceR.Keys.ToList();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(solutionFile))
                {
                    foreach (char key in gamma)
                    {
                        string value = solution.ContainsKey(key) ? solution[key] : instance.RefereceR[key].First();
                        file.WriteLine("{0}:{1}", key, value);
                    }
                }
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }
}
