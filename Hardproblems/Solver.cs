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
            Solve(instance, new Dictionary<char, string>());

            return null;
            //TODO need implementation.
            //throw new NotImplementedException();
        }

        private Dictionary<char, string> Solve(Dekoder instance, Dictionary<char, string> variablesAssigned)
        {
            if (variablesAssigned.Keys.Count == instance.GammaAlphabet.Count)
            {
                if (IsValidSolution(instance, variablesAssigned))
                {
                    return variablesAssigned;
                }
            }
            else
            {
                char variable = instance.GammaAlphabet[variablesAssigned.Count];
                if (variablesAssigned.ContainsKey(variable))
                {
                    throw new Exception("Picked variable does already exists in the assignedVariables. This should never happen.");
                }
                foreach (string RItem in instance.R[variable])
                {
                    Dictionary<char, string> changedDictionary = new Dictionary<char, string>(variablesAssigned);
                    changedDictionary[variable] = RItem;
                    var solution = Solve(instance, changedDictionary);
                    if (solution != null)
                    {
                        return solution;
                    }
                }
            }
            return null;
        }

        private bool IsValidSolution(Dekoder instance, Dictionary<char, string> variablesAssigned)
        {
            //TODO implement
            return false;
        }
    }
}
