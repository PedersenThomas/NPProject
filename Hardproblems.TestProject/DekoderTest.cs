using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hardproblems.TestProject
{
    [TestClass]
    public class DekoderTest
    {
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
                {'A', new List<string> { "a", "b", "c", "d", "e", "f", "dd" }},
                {'B', new List<string> { "a", "b", "c", "d", "e", "f", "dd" }},
                {'C', new List<string> { "a", "b", "c", "d", "e", "f", "dd" }},
                {'D', new List<string> { "a", "b", "c", "d", "e", "f", "dd" }},
                {'E', new List<string> { "aa", "bd", "c", "d", "e"}}
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

        private string ListToString<T>(IEnumerable<T> list)
        {
            return string.Join(",", list.Select(x => x.ToString()).ToArray());
        }

        private void RDictionariesAreEqual(Dictionary<char, List<string>> expected, Dictionary<char, List<string>> actual)
        {
            CollectionAssert.AreEqual(expected.Keys, actual.Keys);
            foreach (KeyValuePair<char, List<string>> pair in expected)
            {
                CollectionAssert.AreEqual(pair.Value, actual[pair.Key]);
            }
        }
    }

}
