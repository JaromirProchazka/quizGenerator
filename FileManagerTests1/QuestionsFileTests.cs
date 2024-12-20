﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizPersistence;
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

namespace FileManagerTests1
{
    [TestClass()]
    public class QuestionsFileTests
    {
        /// <summary>
        /// Fetch the Test Data from a given file.
        /// </summary>
        /// <param name="fileName">Name of the test file including the extension</param>
        /// <returns>Returns the Expected result data from the file given in parameter</returns>
        public static string tData(string fileName)
        {
            var p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", fileName);
            return File.ReadAllText(p, Encoding.UTF8);
        }

        // createQuestionsFile
        [TestMethod()]
        public void createQuestionsFileText_onlyQuestions()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("basicNotes.html")));

            string message = areHtmlsEquivalent(tData(@"basicQuestions.html"), questions);
            Assert.IsTrue(message == "", message);
        }

        [TestMethod()]
        public void createQuestionsFileText_RedundantText()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("redundantNotes.html")));

            string message = areHtmlsEquivalent(tData("basicQuestions.html"), questions);
            Assert.IsTrue(message == "", message);
        }

        [TestMethod()]
        public void createQuestionsFileText_IncludesHeadings()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("headingsNotes.html")));

            string message = areHtmlsEquivalent(tData("headingQuestions.html"), questions);
            Assert.IsTrue(message == "", message);
        }

        [TestMethod()]
        public void createQuestionsFileText_HeadingsAnswers()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("headingAnswersNotes.html")));

            string message = areHtmlsEquivalent(tData("headingAnswersQuestions.html"), questions);
            Assert.IsTrue(message == "", message);
        }

        [TestMethod()]
        public void createQuestionsFileText_RepeatedParsing()
        {
            string questions1 = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("headingAnswersNotes.html")));
            string message1 = areHtmlsEquivalent(tData("headingAnswersQuestions.html"), questions1);
            Assert.IsTrue(message1 == "", message1);

            string questions2 = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("headingsNotes.html")));
            string message2 = areHtmlsEquivalent(tData("headingQuestions.html"), questions2);
            Assert.IsTrue(message2 == "", message2);

            string questions3 = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("redundantNotes.html")));
            string message3 = areHtmlsEquivalent(tData("basicQuestions.html"), questions3);
            Assert.IsTrue(message3 == "", message3);
        }

        [TestMethod()]
        public void createQuestionsFileText_NoNotes()
        {
            string questions = cleanUp(QuestionsFile.CreateQuestionsFileText(tData("noNotes.html")));

            string message = areHtmlsEquivalent(tData("noQuestions.html"), questions);
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
            for (int i = 0; i < htmlCode.Length; i++)
            {
                result += htmlCode[i];
                if (htmlCode[i] == '<')
                {
                    lesserThen = true;
                }
                else if (htmlCode[i] == '/' && lesserThen)
                {
                    endTag = true;
                }
                else
                {
                    lesserThen = false;
                }
                if (htmlCode[i] == '>' && endTag)
                {
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
                    return $"{a.Substring(i - ((i < 10) ? i : 10), a.Length - i)} \n\n {b.Substring(i - ((i < 10) ? i : 10), b.Length - i)}";
                }
            }
            return "";
        }
    }
}