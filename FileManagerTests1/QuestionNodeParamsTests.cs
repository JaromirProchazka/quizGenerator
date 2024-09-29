using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.NotesParsing;
using HtmlAgilityPack;

namespace FileManagerTests.NotesParsingTests
{
    [TestClass()]
    public class QuestionNodeParamsTests
    {
        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeName_CheckFail()
        {
            var analyser = new AndQuestionNodeParams().SetName("em");
            string nodeName = "div";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsFalse(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeName_CheckPass()
        {
            var analyser = new AndQuestionNodeParams().SetName("em");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsTrue(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeAnyClass_CheckFail()
        {
            var analyser = new AndQuestionNodeParams().SetClasses("abc");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsFalse(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeAnyClass_CheckPass()
        {
            var analyser = new AndQuestionNodeParams().SetClasses("container");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsTrue(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeColor_CheckFail()
        {
            var analyser = new AndQuestionNodeParams().SetColor("blue");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsFalse(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeColor_CheckPass()
        {
            var analyser = new AndQuestionNodeParams().SetColor("red");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0;  color: red;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsTrue(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeFont_CheckFail()
        {
            var analyser = new AndQuestionNodeParams().SetFontFamily("sans");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsFalse(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_NodeFont_CheckPass()
        {
            var analyser = new AndQuestionNodeParams().SetFontFamily("garamond");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsTrue(analyser.Analyze(node));
        }





        [TestMethod()]
        public void AnalyzeNodeAttributes_And_CheckFail()
        {
            var analyser = new AndQuestionNodeParams().SetName("em").SetColor("red").SetFontFamily("sans");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsFalse(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_And_CheckPass()
        {
            var analyser = new AndQuestionNodeParams().SetFontFamily("garamond").SetName("em").SetColor("red");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsTrue(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_Or_CheckFail()
        {
            var analyser = new OrQuestionNodeParams().SetName("div").SetColor("blue").SetFontFamily("sans");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsFalse(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_Or_CheckPass()
        {
            var analyser = new OrQuestionNodeParams().SetName("div").SetColor("blue").SetFontFamily("garamond");
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsTrue(analyser.Analyze(node));
        }



        [TestMethod()]
        public void AnalyzeNodeAttributes_Or_Trivial()
        {
            var analyser = new OrQuestionNodeParams();
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsFalse(analyser.Analyze(node));
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_And_Trivial()
        {
            var analyser = new AndQuestionNodeParams();
            string nodeName = "em";
            Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "id", "myDiv" },
                    { "class", "container" },
                    { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                };

            HtmlNode node = CreateHtmlNode(nodeName, attributes);

            Assert.IsTrue(analyser.Analyze(node));
        }



        [TestMethod()]
        public void AnalyzeNodeAttributes_Or_Repeated()
        {
            var analyser = new OrQuestionNodeParams().SetName("div").SetColor("blue").SetFontFamily("garamond");

            {
                string nodeName = "em";
                Dictionary<string, string> attributes = new Dictionary<string, string>
                    {
                        { "id", "myDiv" },
                        { "class", "container" },
                        { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                    };
                HtmlNode node = CreateHtmlNode(nodeName, attributes);
                Assert.IsTrue(analyser.Analyze(node));
            }

            {
                string nodeName1 = "div";
                Dictionary<string, string> attributes1 = new Dictionary<string, string>
                    {
                        { "id", "myDiv" },
                        { "class", "container" },
                        { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, sans,serif;" }
                    };
                HtmlNode node1 = CreateHtmlNode(nodeName1, attributes1);
                Assert.IsTrue(analyser.Analyze(node1));
            }

            {
                string nodeName1 = "p";
                Dictionary<string, string> attributes1 = new Dictionary<string, string>
                    {
                        { "id", "myDiv" },
                        { "class", "container" },
                        { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  blue;  font-family:georgia, sans,serif;" }
                    };
                HtmlNode node1 = CreateHtmlNode(nodeName1, attributes1);
                Assert.IsTrue(analyser.Analyze(node1));
            }

            {
                string nodeName1 = "p";
                Dictionary<string, string> attributes1 = new Dictionary<string, string>
                    {
                        { "id", "myDiv" },
                        { "class", "container" },
                        { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, sans,serif;" }
                    };
                HtmlNode node1 = CreateHtmlNode(nodeName1, attributes1);
                Assert.IsFalse(analyser.Analyze(node1));
            }
        }

        [TestMethod()]
        public void AnalyzeNodeAttributes_And_Repeated()
        {
            var analyser = new AndQuestionNodeParams().SetFontFamily("garamond").SetName("em").SetColor("red");

            {
                string nodeName = "em";
                Dictionary<string, string> attributes = new Dictionary<string, string>
                    {
                        { "id", "myDivv" },
                        { "class", "container" },
                        { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                    };
                HtmlNode node = CreateHtmlNode(nodeName, attributes);
                Assert.IsTrue(analyser.Analyze(node));
            }

            {
                string nodeName = "em";
                Dictionary<string, string> attributes = new Dictionary<string, string>
                    {
                        { "id", "myDiv" },
                        { "class", "containers" },
                        { "style", "width:100%;height:200px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                    };
                HtmlNode node = CreateHtmlNode(nodeName, attributes);
                Assert.IsTrue(analyser.Analyze(node));
            }

            {
                string nodeName = "em";
                Dictionary<string, string> attributes = new Dictionary<string, string>
                    {
                        { "id", "myDiv" },
                        { "class", "container" },
                        { "style", "width:100%;height:400px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                    };
                HtmlNode node = CreateHtmlNode(nodeName, attributes);
                Assert.IsTrue(analyser.Analyze(node));
            }

            {
                string nodeName = "p";
                Dictionary<string, string> attributes = new Dictionary<string, string>
                    {
                        { "id", "myDiv" },
                        { "class", "container" },
                        { "style", "width:100%;height:400px;background-color:#f0f0f0; color:  red;  font-family:georgia, garamond,serif;" }
                    };
                HtmlNode node = CreateHtmlNode(nodeName, attributes);
                Assert.IsFalse(analyser.Analyze(node));
            }
        }








        // Phind - generated
        private static HtmlNode CreateHtmlNode(string nodeName, Dictionary<string, string> attributes = null)
        {
            var doc = new HtmlDocument();
            var node = doc.CreateElement(nodeName);

            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    node.SetAttributeValue(attribute.Key, attribute.Value);
                }
            }

            return node;
        }
    }
}
