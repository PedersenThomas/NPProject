using System;
using System.Collections.Generic;

namespace Hardproblems
{
    public class Solver
    {
        public Dictionary<char, string> Solve(Dekoder instance)
        {
            Dictionary<char, string> solution = Solve(instance, new Dictionary<char, string>());

            return solution;
        }
        

        private Dictionary<char, string> Solve(Dekoder instance, Dictionary<char, string> variablesAssigned)
        {
            foreach (string stringT in instance.T) // Check if all T string have length less that S
            {
                if (stringT.Length > instance.S.Length)
                {
                    return null;
                }
            }
            
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
                foreach (string rItem in instance.R[variable])
                {
                    Dictionary<char, string> changedDictionary = new Dictionary<char, string>(variablesAssigned);
                    changedDictionary[variable] = rItem;
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
            foreach (String t in instance.T)
            {
                String replacedT = String.Copy(t);
                foreach (char letter in replacedT)
                {
                    if (letter >= 'A' && letter <= 'Z' && variablesAssigned.ContainsKey(letter))
                    {
                        replacedT = replacedT.Replace(letter.ToString(), variablesAssigned[letter]);
                    }
                }

                if (!instance.S.Contains(replacedT))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
