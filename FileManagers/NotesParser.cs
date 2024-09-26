using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileManager
{ 
    internal record class NotesParser
    {
        internal static string baseMarkdown = "<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" /><title></title><script type=\"text/javascript\" language=\"javascript\" src=\".\\..\\" + QuestionsFile.scriptFileName + "\"></script></head><body></body><link rel=\"stylesheet\" href=\".\\..\\" + QuestionsFile.stylesFileName + "\" /></html>";
        internal static string questionTemplate = "<div class=\"question_box\" id=\"question_0\" style=\"display: none\"><h1 class=\"question_name\"></h1><div class=\"question_answer\" style=\"display: none\"></div></div>";
        internal static string headingTemplate = "<div class=\"heading_sections\"><h1 class=\"section_heading\" style=\"display: none\"></h1><div class=\"heading_section_contents\" style=\"display: none\"></div></div>";

        NotesParsingState state = new NotesParsingState();
        QuizFileResult resultState;

        public NotesParser()
        {
            resultState = new QuizFileResult(state);
        }

        /// <summary>
        /// Takes a html file with notes, and creates a corresponding html markdown contents of just the quiz parts of the notes in standartised format.
        /// Also uses a config file for the specifics of notes creation.
        /// </summary>
        public string CreateQuestionsFileText(string notesMarkdown)
        {
            HtmlDocument notes = new HtmlDocument();
            HtmlDocument result = new HtmlDocument();
            notes.LoadHtml(notesMarkdown);
            result.LoadHtml(baseMarkdown);
            resultState.resultCurrentPosition = result.DocumentNode.SelectSingleNode("//body");

            HtmlNode bodyNode = notes.DocumentNode.SelectSingleNode("//body");
            goThrowNodes(bodyNode);

            string resultText = result.DocumentNode.OuterHtml;
            state.Reset();
            resultState.Reset();
            return resultText;
        }

        void goThrowNodes(HtmlNode node)
        {
            HtmlNodeCollection children = node.ChildNodes;
            bool areInHeading = false;
            HtmlNode currentHeadingAnswer = null;
            foreach (HtmlNode child in children)
            {
                onNode(child, ref areInHeading, ref currentHeadingAnswer);
            }
        }

        void onNode(HtmlNode child, ref bool areInHeading, ref HtmlNode currentHeadingAnswer)
        {
            areInHeading = false;
            if (child.NodeType != HtmlNodeType.Element)
            {
                return;
            }

            if (child.Name == "header")
            {
                return;
            }

            if (resultState.isHeading(child))
            {
                resultState.addHeading(child);
                areInHeading = true;
                state.noQuestionInHeadingYet = true;
                currentHeadingAnswer = resultState.resultCurrentPosition.SelectSingleNode("//*[contains(@class, 'heading_section_contents')]");
            }
            else if (child.Name == "em")
            {
                state.noQuestionInHeadingYet = false;
                resultState.addQuestion(child);
            }

            if (!areInHeading && child.Name != "em")
            {
                goThrowNodes(child);
            }

            if (state.currentParentHeadingRank != 0 && state.noQuestionInHeadingYet && !areInHeading && currentHeadingAnswer != null)
            {
                currentHeadingAnswer.AppendChild(child);
            }
        }

        /// <summary>
        /// Gets a title of html page.
        /// </summary>
        /// <param name="htmlFile">html page</param>
        /// <returns>title of html page</returns>
        public static string getHtmlTitle(HtmlDocument htmlFile)
        {
            HtmlNode title = htmlFile.DocumentNode.SelectSingleNode("//title");
            if (title != null)
            {
                return title.InnerText;
            }
            return "";
        }
    }

    internal record class QuizFileResult
    {
        public HtmlNode? resultCurrentPosition;
        private NotesParsingState state;

        public QuizFileResult(NotesParsingState state) 
        {
            this.state = state;
            Reset();
        }

        public void Reset()
        {
            resultCurrentPosition = null;
        }

        internal void addQuestion(HtmlNode question)
        {
            if (question == null) { return; }
            HtmlDocument questionBoxTemplate = new HtmlDocument();
            questionBoxTemplate.LoadHtml(NotesParser.questionTemplate);
            addIdCounterToNodeInDoc(questionBoxTemplate);

            HtmlNode latestQuestionBox = questionBoxTemplate
                .DocumentNode
                .SelectSingleNode("//*[contains(@class, 'question_box')]");
            HtmlNode questionBox = resultCurrentPosition
                .AppendChild(latestQuestionBox);

            HtmlNode questionName = questionBox.SelectSingleNode("//*[contains(@class, 'question_name')]");
            HtmlNode questionAnswear = questionBox
                .SelectSingleNode("//*[contains(@class, 'question_answer')]");
            if (questionAnswear != null && questionName != null)
            {
                questionBox.SelectSingleNode("//*[contains(@class, 'question_name')]").InnerHtml = question.InnerText;
                questionAnswear.AppendChild(ParentTextNode(question));
            }
        }

        internal void addHeading(HtmlNode heading)
        {
            if (heading == null) { return; }
            HtmlDocument headingBoxTemplate = new HtmlDocument();
            headingBoxTemplate.LoadHtml(NotesParser.headingTemplate);
            addIdCounterToNodeInDoc(headingBoxTemplate);
            int headingRank = getHeadingRank(heading);

            if (headingRank <= state.currentParentHeadingRank)
            {
                HtmlNode currentParentHeading = resultCurrentPosition.ParentNode;
                int parentHeadingRank = state.currentParentHeadingRank - 1;
                while (parentHeadingRank >= headingRank)
                {
                    currentParentHeading = currentParentHeading.ParentNode;
                    parentHeadingRank--;
                }
                resultCurrentPosition = currentParentHeading;
            }
            HtmlNode headingBox = resultCurrentPosition.AppendChild(headingBoxTemplate.DocumentNode.SelectSingleNode("//div"));
            resultCurrentPosition = headingBox.SelectSingleNode("//*[contains(@class, 'heading_sections')]");
            state.currentParentHeadingRank = headingRank;

            headingBox.SelectSingleNode("//*[contains(@class, 'section_heading')]").InnerHtml = heading.InnerText;
        }

        void addIdCounterToNodeInDoc(HtmlDocument docForId)
        {
            docForId.DocumentNode
                .SelectSingleNode("//div")
                .SetAttributeValue("id", $"question_{state.idCounter}");
            state.idCounter++;
        }

        private HtmlNode ParentTextNode(HtmlNode keySegment)
        {
            string[] blockElements = { "p", "ul", "ol", "pre", "div", "blockquote", "dl", "figure", "hr" };
            HtmlNode parent = keySegment;
            while (!blockElements.Contains(parent.Name) && !isHeading(parent))
            {
                parent = parent.ParentNode;
            }
            return parent;
        }

        internal bool isHeading(HtmlNode node)
        {
            return getHeadingRank(node) > 0;
        }

        /// <summary>
        /// Takes a HtmlNode and if it is a heading like 'h3', returns it's rank, in our situation '3' or it returns '0', if it isn't a heading node.
        /// </summary>
        private int getHeadingRank(HtmlNode hx)
        {
            int rank = 0;
            if (hx.Name[0] != 'h')
            {
                return 0;
            }

            try
            {
                rank = Convert.ToInt32(hx.Name.Substring(1, hx.Name.Length - 1));
            }
            catch
            {
                return 0;
            }

            return rank;
        }
    }

    internal record class NotesParsingState
    {
        public int idCounter;
        public int currentParentHeadingRank;
        public bool noQuestionInHeadingYet;
        

        public NotesParsingState()
        {
            Reset();
        }

        public void Reset()
        {
            idCounter = 1;
            currentParentHeadingRank = 0;
            noQuestionInHeadingYet = true;
        }
    }
}
