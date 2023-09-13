using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;

namespace FileManager.Tests
{
    [TestClass()]
    public class QuestionsFileTests
    {
        // createQuestionsFile
        [TestMethod()]
        public void createQuestionsFileText_onlyQuestions()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(File.ReadAllText(@"..\..\..\basicNotes.html")));

            string message = areHtmlsEquivalent(File.ReadAllText(@"..\..\..\basicQuestions.html", Encoding.UTF8), questions);
            Assert.IsTrue(message == "", message);
        }
        [TestMethod()]
        public void createQuestionsFileText_RedundantText()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(File.ReadAllText(@"..\..\..\redundantNotes.html")));

            string message = areHtmlsEquivalent(File.ReadAllText(@"..\..\..\basicQuestions.html", Encoding.UTF8), questions);
            Assert.IsTrue(message == "", message);
        }
        [TestMethod()]
        public void createQuestionsFileText_IncludesHeadings()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(File.ReadAllText(@"..\..\..\headingsNotes.html")));

            string message = areHtmlsEquivalent(File.ReadAllText(@"..\..\..\headingQuestions.html", Encoding.UTF8), questions);
            Assert.IsTrue(message == "", message);
        }
        [TestMethod()]
        public void createQuestionsFileText_NoNotes()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(File.ReadAllText(@"..\..\..\noNotes.html")));

            string message = areHtmlsEquivalent(File.ReadAllText(@"..\..\..\noQuestions.html", Encoding.UTF8), questions);
            Assert.IsTrue(message == "", message);
        }

        string areHtmlsEquivalent(string originalHtml, string copyHtml)
        {
            originalHtml = cleanUp(originalHtml);
            if (originalHtml.SequenceEqual(copyHtml))
            {
                return "";
            }

            string difference = "";
            var originalDoc = new HtmlDocument();
            var copyDoc = new HtmlDocument();
            originalDoc.LoadHtml(originalHtml);
            copyDoc.LoadHtml(copyHtml);

            var copyBody = copyDoc.DocumentNode.SelectSingleNode("//body");
            if (copyBody == null) { return $"The copy has no body Node."; }
            CompareNodesOnSamePlace(
                originalDoc.DocumentNode.SelectSingleNode("//body"),
                copyBody
            );

            return difference;

            void CompareNodesOnSamePlace(HtmlNode originalNode, HtmlNode copyNode)
            {
                if (difference != "") { return; }
                if (originalNode.NodeType != copyNode.NodeType) 
                {
                    difference = $"Node in original: '{makeHtmlReadable(originalNode.OuterHtml)}' \n\n is not same as equivalent from copy: '{makeHtmlReadable(copyNode.OuterHtml)}'. \n\n diff: '{makeHtmlReadable(findDifference(originalNode.OuterHtml, copyNode.OuterHtml))}'.\n\n";
                    return; 
                }
                if (originalNode.ChildNodes.Count == 0)
                {
                    compareTags(originalNode, copyNode);
                    compareAttributes(originalNode, copyNode);
                    if (difference != "") { return; }
                    if (originalNode.InnerText != copyNode.InnerText)
                    {
                        difference = $"Node in original: '{makeHtmlReadable(originalNode.OuterHtml)}' \n\n is not same as equivalent from copy: '{makeHtmlReadable(copyNode.OuterHtml)}'. \n\n diff: '{makeHtmlReadable(findDifference(originalNode.OuterHtml, copyNode.OuterHtml))}'.\n\n";
                        return;
                    }
                }

                for (int i = 0; i < originalNode.ChildNodes.Count; i++)
                {
                    if (i > copyNode.ChildNodes.Count - 1)
                    {
                        difference = $"Node in original: '{makeHtmlReadable(originalNode.OuterHtml)}' \n\n is not same as equivalent from copy: '{makeHtmlReadable(copyNode.OuterHtml)}'. \n\n diff: '{makeHtmlReadable(findDifference(originalNode.OuterHtml, copyNode.OuterHtml))}'.\n\n";
                        return;
                    }
                    compareTags(originalNode, copyNode);
                    compareAttributes(originalNode, copyNode);
                    CompareNodesOnSamePlace(originalNode.ChildNodes[i], copyNode.ChildNodes[i]);
                    if (difference != "") { return; }
                }


                void compareAttributes(HtmlNode originalNodeForAttributes, HtmlNode copyNodeForAttributes)
                {
                    if (difference != "") { return; }
                    HtmlAttributeCollection originalAttributes = originalNodeForAttributes.Attributes;
                    HtmlAttributeCollection copyAttributes = copyNodeForAttributes.Attributes;
                    if (copyAttributes.Count != originalAttributes.Count)
                    {
                        difference = $"Node in original: '{makeHtmlReadable(originalNode.OuterHtml)}' \n\n is not same as equivalent from copy: '{makeHtmlReadable(copyNode.OuterHtml)}'. \n\n diff: '{makeHtmlReadable(findDifference(originalNode.OuterHtml, copyNode.OuterHtml))}'.\n\nIn copyHtml, node with contents '{copyNodeForAttributes.OuterHtml}' has different attributes from the original '{originalNodeForAttributes.OuterHtml}'.";
                    }
                    for (int i = 0; i < originalNode.Attributes.Count; i++)
                    {
                        HtmlAttribute copyAttribute = copyAttributes[i];
                        HtmlAttribute originalAttribute = originalAttributes[i];

                        if (originalAttribute.Name != copyAttribute.Name || originalAttribute.Value != copyAttribute.Value)
                        {
                            difference = $"Codes Nodes doesnt share attributes, node with contents of '{originalNodeForAttributes.OuterHtml}' has attribute '{originalAttribute.Name}: {originalAttribute.Value}', while copy node has attribute '{copyAttribute.Name}: {copyAttribute.Value}'.";
                            return;
                        }
                    }
                }

                void compareTags(HtmlNode originalNodeForTags, HtmlNode copyNodeForTags)
                {
                    if (difference != "") { return; }
                    if (originalNodeForTags.Name != copyNodeForTags.Name)
                    {
                        difference = $"Node in original: '{makeHtmlReadable(originalNode.OuterHtml)}' \n\n is not same as equivalent from copy: '{makeHtmlReadable(copyNode.OuterHtml)}'. \n\n diff: '{makeHtmlReadable(findDifference(originalNode.OuterHtml, copyNode.OuterHtml))}'.\n\n The node with contents '{copyNodeForTags.OuterHtml}' has node type '{copyNodeForTags.Name}' but should have '{originalNodeForTags.Name}'.";
                    }
                }
            }
        }
        private string cleanUp(string htmlCode)
        {
            string newOriginalHtml = "";
            bool nFound = false;
            for (int i = 0; i < htmlCode.Length; i++)
            {
                if (htmlCode[i] != '\n' && !(nFound && htmlCode[i] == ' '))
                {
                    newOriginalHtml += htmlCode[i];
                    nFound = false;
                }
                else
                {
                    nFound = true;
                }
            }
            return newOriginalHtml;
        }

        private string makeHtmlReadable(string htmlCode)
        {
            string result = "";
            bool lesserThen = false;
            bool endTag = false;
            for (int i = 0;i < htmlCode.Length;i++)
            {
                result += htmlCode[i];
                if (htmlCode[i] == '<')
                {
                    lesserThen = true;
                }
                else if (htmlCode[i] == '/' && lesserThen)
                {
                    endTag = true;
                } else
                {
                    lesserThen = false;
                }
                if (htmlCode[i] ==  '>' && endTag) {
                    result += "\n";
                    endTag = false;
                }
            }
            return result;
        }

        private string findDifference(string a, string b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (b.Length > i && a[i] != b[i])
                {
                    return $"{a.Substring(i - ((i<10)?i:10), a.Length - i)} \n\n {b.Substring(i - ((i < 10) ? i : 10), b.Length - i)}";
                }
            }
            return "";
        }
    }

    [TestClass()]
    public class SequenceOfQuestionsTests
    {
        [TestMethod()]
        public void buildQuestionDag_onlyBasicQuestions()
        {
            sequenceOfQuestions questions = new sequenceOfQuestions(File.ReadAllText(@"..\..\..\basicQuestions.html"));
            List<string> sequence = questions.getSequence();
            int checkCounter = 0;
            string dagS = "";
            foreach (string s in sequence)
            {
                dagS += s + " ";
            }

            if (sequence.Count != 3)
            {
                Assert.Fail($"More or Less questions than neccesary (is {sequence.Count} but should be 3).");
            }
            foreach (string s in new string[] { "question_1", "question_2", "question_3" })
            {
                checkCounter += (sequence.Contains(s)) ? 1 : 0;
            }

            if (checkCounter != 3)
            {
                Assert.Fail($"Missing item, ({dagS})");
            }
        }

        [TestMethod()]
        public void buildQuestionDag_headingsQuestions()
        {
            sequenceOfQuestions questions = new sequenceOfQuestions(File.ReadAllText(@"..\..\..\shortHeadingQuestion.html"));
            List<string> sequence = questions.getSequence();
            string[] dagIds = new string[] { "question_1", "question_2", "question_3", "question_4" };
            if (sequence.Count != 4)
            {
                Assert.Fail($"The sequence must have 4 items, but has {sequence.Count}.");
            }
            foreach (string s in dagIds)
            {
                if (!sequence.Contains(s))
                {
                    Assert.Fail($"Question {s} is missing.");
                }
            }

            if (sequence.Last() == "question_4")
            {
                Assert.Fail("BAD ordering, last must not be 'question_4'.");
            }
            sequence.Remove("question_4");
            if (!sequence.SequenceEqual(new List<string>(new string[] { "question_3", "question_2", "question_1", })))
            {
                Assert.Fail($"BAD ordering, first three must be 'question_3, question_2, question_1' but us '{sequence[0]}, {sequence[1]}, {sequence[2]}'.");
            }
        }

        [TestMethod()]
        public void buildQuestionsDag_NoQuestions()
        {
            sequenceOfQuestions questions = new sequenceOfQuestions(File.ReadAllText(@"..\..\..\noQuestions.html"));
            List<string> sequence = questions.getSequence();

            Assert.IsTrue(sequence.Count == 0, $"Should be empty, but is {sequence}");
        }
    }

    [TestClass()]
    public class DAGTests
    {
        [TestMethod()]
        public void buildDug_topologicalOrderCorrectness()
        {
            Dag result = new Dag();

            result.insert("q0", new string[0] );
            result.insert("q1", new string[] { "q0" } );
            result.insert("q2", new string[] { "q0" , "q1" } );
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
            for ( int i = 0; i < 300; i++)
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

            if (!possibilityOccured.SequenceEqual(allTrue)) {
                int? firsUnoccured = null;
                for (int i = 0; i < possibilityOccured.Length; i++) { 
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
            char[] alphabet = new char[] {'a', 'b', 'c', 'd', 'e'};
            char[] result = new char[alphabet.Length];

            q.Enque(1, alphabet[1]);
            q.Enque(0, alphabet[0]);
            q.Enque(4, alphabet[4]);
            q.Enque(2, alphabet[2]);
            q.Enque(3, alphabet[3]);

            for (int i = 0;i < result.Length;i++)
            {
                result[i] = q.Pop();
            }

            for (int i = 0; i < result.Length;i++) {
                Assert.AreEqual(alphabet[i], result[i], 
                    "Inconsistenci on index: " + i + ", in result is: " + result[i] + ", but should be: " + alphabet[i]);
            }
        }
    }
}