using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hardproblems.TestProject
{
    [TestClass]
    public class DekoderTest
    {
        private const int second = 1000;
        private const int minute = 60*second;
        //In miliseconds
        private const int Timeout = 30 * minute;

        [TestMethod]
        public void DekoderTest_ReferenceProblem()
        {
            const string text =
@"4
abdde
ABD
DDE
AAB
ABd
A:a,b,c,d,e,f,dd
B:a,b,c,d,e,f,dd
C:a,b,c,d,e,f,dd
D:a,b,c,d,e,f,dd
E:aa,bd,c,d,e";
            Dekoder dekoder = new Dekoder();
            dekoder.ReadFromText(text);

            Assert.AreEqual("abdde", dekoder.S);

            List<string> expectedK = new List<string> { "ABD", "DDE", "AAB", "ABd" };
            CollectionAssert.AreEqual(expectedK, dekoder.T, String.Format("Expected: {0}\nActual: {1}", ListToString(expectedK), ListToString(dekoder.T)));

            var dict = new Dictionary<char, List<string>>
            {
                {'A', new List<string> { "a", "b", "d", "e", "dd" }},
                {'B', new List<string> { "a", "b", "d", "e", "dd" }},
                {'D', new List<string> { "a", "b", "d", "e", "dd" }},
                {'E', new List<string> { "bd", "d", "e"}}
            };
            Debug.WriteLine("{0}:{1}", dict.First().Key, ListToString(dict.First().Value));
            RDictionariesAreEqual(dict, dekoder.R);
        }

        [TestMethod]
        public void DekoderTestFileParseAble()
        {
            for (int i = 1; i <= 6; i++)
            {
                string filename = string.Format("testset/test{0:D2}.SWE", i);
                Dekoder dekoder = new Dekoder();
                dekoder.ReadFromFile(filename);
                Assert.IsNotNull(dekoder.S);
            }
        }

        [TestMethod, Timeout(Timeout)]
        public void SolveFile_test01()
        {
            string filename = "testset/test01.SWE";
            var solution = runSolverFromFile(filename);
            Assert.IsNull(solution);
        }

        [TestMethod, Timeout(Timeout)]
        public void SolveFile_test02()
        {
            string filename = "testset/test02.SWE";
            var solution = runSolverFromFile(filename);
            Assert.IsNotNull(solution);
        }

        [TestMethod, Timeout(Timeout)]
        public void SolveFile_test03()
        {
            string filename = "testset/test03.SWE";
            var solution = runSolverFromFile(filename);
            Assert.IsNull(solution);
        }

        [TestMethod, Timeout(Timeout)]
        public void SolveFile_test04()
        {
            string filename = "testset/test04.SWE";
            var solution = runSolverFromFile(filename);
            Assert.IsNotNull(solution);
        }

        [TestMethod, Timeout(Timeout)]
        public void SolveFile_test05()
        {
            string filename = "testset/test05.SWE";
            var solution = runSolverFromFile(filename);
            Assert.IsNull(solution);
        }

        [TestMethod, Timeout(Timeout)]
        public void SolveFile_test06()
        {
            string filename = "testset/test06.SWE";
            var solution = runSolverFromFile(filename);
            Assert.IsNotNull(solution);
        }

        private Dictionary<char, string> runSolverFromFile(string filename)
        {
            Dekoder dekoder = new Dekoder();
            dekoder.ReadFromFile(filename);
            Solver solver = new Solver();
            var solution = solver.Solve(dekoder);
            return solution;
        }

        [TestMethod]
        public void SimpleValid()
        {
            string filename = "testset/test07.SWE";
            Dekoder instance = new Dekoder();
            instance.ReadFromFile(filename);
            Solver s = new Solver();
            var solution = s.Solve(instance);

            Assert.IsNotNull(solution);

            foreach (KeyValuePair<char, List<string>> keyValuePair in instance.R)
            {
                Debug.WriteLine("{0}: {1}", keyValuePair.Key, String.Join(",", keyValuePair.Value));
            }
        }

        private string ListToString<T>(IEnumerable<T> list)
        {
            return string.Join(",", list.Select(x => x.ToString()).ToArray());
        }

        private void RDictionariesAreEqual(Dictionary<char, List<string>> expected, Dictionary<char, List<string>> actual)
        {
            CollectionAssert.AreEqual(expected.Keys, actual.Keys, "Expected: {0} -> Actual: {1}", String.Join(",", expected.Keys), String.Join(",", actual.Keys));
            foreach (KeyValuePair<char, List<string>> pair in expected)
            {
                CollectionAssert.AreEqual(pair.Value, actual[pair.Key]);
            }
        }
    }

}
