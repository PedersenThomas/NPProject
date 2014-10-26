using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardproblems
{
    public class Solver
    {
        public List<string> Solve(Dekoder instance)
        {
            solve(instance, new Dictionary<char, string>());

            //TODO need implementation.
            throw new NotImplementedException();
        }

        private void solve(Dekoder instance, Dictionary<char, string> variablesAssigned)
        {
            foreach (char variable in instance.GammaAlphabet)
            {
                if (!variablesAssigned.ContainsKey(variable))
                {
                    foreach (string RItem in instance.R[variable])
                    {
                        variablesAssigned[variable] = RItem;
                    }
                }
            }
        }

        private bool IsValidSolution(Dekoder instance, Dictionary<char, string> variablesAssigned)
        {
            //TODO implement
            return false;
        }
    }
}
