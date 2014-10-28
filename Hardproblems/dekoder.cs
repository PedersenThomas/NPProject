using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hardproblems
{
    public class Dekoder
    {
        public List<string> T { get; private set; }
        public Dictionary<char,List<string>> R { get; private set; }
        public string S { get; private set; }

        public List<char> GammaAlphabet
        {
            get { return R.Keys.ToList(); }
        }

        public void ReadFromFile(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                String text = sr.ReadToEnd();
                ReadFromText(text);
            }
        }

        public void ReadFromStdIn()
        {
            int numK;
            string lineK = Console.ReadLine();
            if (!int.TryParse(lineK.Trim(), out numK))
            {
                throw new FormatException("The first parameter must be an integer, not: " + lineK);
            }

            //Reads in the search string
            S = Console.ReadLine().Trim();

            Regex sigma = new Regex("^[a-z]*$");
            Regex gamma = new Regex("^[A-Z]*$");
            Regex sigmaAndGamma = new Regex("^[a-zA-Z]*$");
            if (!sigma.IsMatch(S))
            {
                throw new FormatException("The problem does not contain enough information");
            }

            T = new List<string>();
            //Set of found keys, for later validation.
            ISet<char> foundGammaLetters = new HashSet<char>();
            //Reads the Ts
            string line;
            for (int i = 0; i < numK; i++)
            {
                line = Console.ReadLine().Trim();
                if (!sigmaAndGamma.IsMatch(line))
                {
                    throw new FormatException("The t is not part of the sigma or gamma alphabet. Line: " + line);
                }
                foreach (char c in line)
                {
                    if (gamma.IsMatch(c + ""))
                    {
                        foundGammaLetters.Add(c);

                    }
                }
                if (!T.Contains(line))
                {
                    T.Add(line);
                }
            }

            R = new Dictionary<char, List<string>>();
            //Reads the Rs.
            while ((line = Console.ReadLine()) != null && line != "")
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                char index = line.First();
                if (!gamma.IsMatch(index + ""))
                {
                    throw new FormatException("The Rs must start with an letter from the gamma alphabet. Actual: " + index);
                }
                if (R.ContainsKey(index))
                {
                    throw new FormatException("The same Key for one of the R sets, is present multiple times. Key: " + index);
                }
                List<string> values = line.Substring(2).Split(',').Select(s => s.Trim()).Where(s => S.Contains(s)).ToList();
                if (values.Distinct().Count() != values.Count())
                {
                    throw new FormatException("The Rs should be an set. Values: " + line.Substring(2));
                }
                foreach (string value in values)
                {
                    if (!sigmaAndGamma.IsMatch(value))
                    {
                        throw new FormatException("The R set items have letter outside the sigma & gamma alphabet. Value: \"" + value + "\"");
                    }
                }

                foreach (string t in T) // removing R which are not in Ts
                {
                    if (t.Contains(index))
                    {
                        R[index] = values;
                        break;
                    }
                }
                //R[index] = values;
            }
            //Checks that every found R key in T, do exsists in R.
            foreach (char letter in foundGammaLetters)
            {
                if (!R.ContainsKey(letter))
                {
                    throw new FormatException("One of the Ts contains a key not given. Key: " + letter);
                }
            }


        }

        /// <summary>
        /// Dekodes the given text, an populate the properties.
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="FormatException"></exception>
        public void ReadFromText(string text)
        {
            int numK;
            if (String.IsNullOrWhiteSpace(text))
            {
                throw new FormatException("The text cannot be empty");
            }
            var lines = text.Split('\n');
            if (lines.Length < 4)
            {
                throw new FormatException("The problem does not contain enough information");
            }
            //Reads in the first parameter T, that tells the number of Ts to come.
            if (!int.TryParse(lines[0].Trim(), out numK))
            {
                throw new FormatException("The first parameter must be an integer, not: " + lines[0]);
            }

            //Reads in the search string
            S = lines[1].Trim();
            Regex sigma = new Regex("^[a-z]*$");
            Regex gamma = new Regex("^[A-Z]*$");
            Regex sigmaAndGamma = new Regex("^[a-zA-Z]*$");
            if (!sigma.IsMatch(S))
            {
                throw new FormatException("The problem does not contain enough information");
            }

            T = new List<string>();
            //Set of found keys, for later validation.
            ISet<char> foundGammaLetters = new HashSet<char>();
            //Reads the Ts
            for (int i = 0; i < numK; i++)
            {
                string line = lines[i + 2].Trim();
                if (!sigmaAndGamma.IsMatch(line))
                {
                    throw new FormatException("The t is not part of the sigma or gamma alphabet. Line: " + line);
                }
                foreach (char c in line)
                {
                    if (gamma.IsMatch(c + ""))
                    {
                        foundGammaLetters.Add(c);
                        
                    }
                }
                if (!T.Contains(line)) {
                    T.Add(line);
                }
            }

            R = new Dictionary<char, List<string>>();
            //Reads the Rs.
            for (int i = 2+numK; i < lines.Length; i++)
            {
                string line = lines[i];
                if (String.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                char index = line.First();
                if (!gamma.IsMatch(index + ""))
                {
                    throw new FormatException("The Rs must start with an letter from the gamma alphabet. Actual: " + index);
                }
                if (R.ContainsKey(index))
                {
                    throw new FormatException("The same Key for one of the R sets, is present multiple times. Key: " + index);
                }
                List<string> values = line.Substring(2).Split(',').Select(s => s.Trim()).Where(s => S.Contains(s)).ToList();
                if (values.Distinct().Count() != values.Count())
                {
                    throw new FormatException("The Rs should be an set. Values: " + line.Substring(2));
                }
                foreach (string value in values)
                {
                    if (!sigmaAndGamma.IsMatch(value))
                    {
                        throw new FormatException("The R set items have letter outside the sigma & gamma alphabet. Value: \"" + value + "\"");
                    }
                }

                foreach (string t in T) // removing R which are not in Ts
                {
                    if (t.Contains(index))
                    {
                        R[index] = values;
                        break;
                    }
                }
                //R[index] = values;
            }
            //Checks that every found R key in T, do exsists in R.
            foreach (char letter in foundGammaLetters)
            {
                if (!R.ContainsKey(letter))
                {
                    throw new FormatException("One of the Ts contains a key not given. Key: " + letter);
                }
            }
        }
    }
}
