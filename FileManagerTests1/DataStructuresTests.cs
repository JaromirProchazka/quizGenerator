using FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesParsing.DataStructures;

namespace FileManagerTests1
{
    [TestClass()]
    public class DAGTests
    {
        [TestMethod()]
        public void buildDug_topologicalOrderCorrectness()
        {
            Dag result = new Dag();

            result.insert("q0", new string[0]);
            result.insert("q1", new string[] { "q0" });
            result.insert("q2", new string[] { "q0", "q1" });
            result.insert("q3", new string[] { "q1" });
            List<String> topologicalOrder = result.randomTopologicalOrder();

            Assert.IsTrue(
                topologicalOrder.SequenceEqual(new List<string>(new string[] { "q0", "q1", "q2", "q3" })) ||
                topologicalOrder.SequenceEqual(new List<string>(new string[] { "q0", "q1", "q3", "q2" })),
                "given order isn't valid: " + topologicalOrder.ToString()
            );
        }

        [TestMethod()]
        public void buildDug_topologicalOrderRandomness()
        {
            Dag result = new Dag();

            result.insert("q0", new string[0]);
            result.insert("q1", new string[] { "q0" });
            result.insert("q2", new string[] { "q0" });
            result.insert("q3", new string[] { "q2" });

            List<string>[] possibilities = {
                new List<string>(new string[] { "q0", "q1", "q2", "q3" } ),
                new List<string>(new string[] { "q0", "q2", "q1", "q3" } ),
                new List<string>(new string[] { "q0", "q2", "q3", "q1" } ),
            };
            bool[] possibilityOccured = { false, false, false };
            bool[] allTrue = { true, true, true };

            List<String> topologicalOrder;
            for (int i = 0; i < 300; i++)
            {
                topologicalOrder = result.randomTopologicalOrder();
                for (int j = 0; j < possibilities.Length; j++)
                {
                    if (topologicalOrder.SequenceEqual(possibilities[j]))
                    {
                        possibilityOccured[j] = true;
                        break;
                    }
                }

                if (possibilityOccured.SequenceEqual(allTrue)) { break; }
            }

            if (!possibilityOccured.SequenceEqual(allTrue))
            {
                int? firsUnoccured = null;
                for (int i = 0; i < possibilityOccured.Length; i++)
                {
                    if (!possibilityOccured[i]) { firsUnoccured = i; break; }
                }
                Assert.Fail("Some possible order hasn't occured, possibility index: " + firsUnoccured);
            }
        }
    }

    [TestClass()]
    public class PriorityQueueTests
    {

        [TestMethod()]
        public void buildQueue_correctOrder()
        {
            PriorityQueue<char> q = new PriorityQueue<char>();
            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e' };
            char[] result = new char[alphabet.Length];

            q.Enque(1, alphabet[1]);
            q.Enque(0, alphabet[0]);
            q.Enque(4, alphabet[4]);
            q.Enque(2, alphabet[2]);
            q.Enque(3, alphabet[3]);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = q.Pop();
            }

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(alphabet[i], result[i],
                    "Inconsistenci on index: " + i + ", in result is: " + result[i] + ", but should be: " + alphabet[i]);
            }
        }
    }
}
