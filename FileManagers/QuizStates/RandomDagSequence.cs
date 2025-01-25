using QuizPersistence;
using QuizPersistence.DataStructures;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Collections;

namespace QuizPersistence.QuizStates
{
    public interface ISequenceOfQuestions
    {
        public List<string> getSequence();
    }

    /// <summary>
    /// A class used to get a random sequence of questions. 
    /// The questions implemented as HTML ID strings, which refer to the questions elements in the markdown provided in constructor.
    /// <example>
    /// <code>
    /// FileManager.sequenceOfQuestions questions = new FileManager.sequenceOfQuestions(File.ReadAllText(path));
    /// var listOfRandomQuestions = questions.getSequence();
    /// </code>
    /// </example>
    /// </summary>
    public class RandomDagSequence : ISequenceOfQuestions
    {
        protected Dag questionSequence = new Dag();

        /// <summary>
        /// Initiates the questions to the Sequence from raw markdown.
        /// </summary>
        /// <param name="questionsMarkdown">the html markdown with the questions data</param>
        public RandomDagSequence(string questionsMarkdown)
        {
            HtmlDocument questions = new HtmlDocument();
            questions.LoadHtml(questionsMarkdown);
            insertChildren(questions.DocumentNode.
                SelectSingleNode("//body")
            );
        }

        /// <summary>
        /// Initiates the questions to the Sequence from file with a markdown.
        /// </summary>
        /// <param name="currentQuestionsPath">FileInfo to the .html markdown with the questions data</param>
        public RandomDagSequence(FileInfo currentQuestionsPath) :
            this(QuestionsFile.GetMarkDown(currentQuestionsPath.FullName))
        { }

        /// <summary>
        /// takes the Processed notes html file (output of FileManager.QuestionsFile.createQuestionsFile) and outputs a JSON file contents with a DAG represented as the List of neighbours.
        /// <example>
        /// <code>
        /// FileManager.sequenceOfQuestions questions = new FileManager.sequenceOfQuestions(File.ReadAllText(path));
        /// var listOfRandomQuestions = questions.getSequence();
        /// </code>
        /// </example>
        /// </summary>
        public List<string> getSequence()
        {
            return questionSequence.randomTopologicalOrder();
        }

        protected virtual void insertChildren(HtmlNode node)
        {
            HtmlNodeCollection children = node.ChildNodes;
            List<string> putInNodes = new List<string>();
            foreach (HtmlNode child in children)
            {
                if (child.HasClass("question_box"))
                {
                    putInNodes.Add(child.Id);
                    questionSequence.insert(child.Id, new string[] { });
                }
                if (child.HasClass("heading_sections"))
                {
                    if (child.Id != null && Regex.IsMatch(child.Id, @"^question_\d+$")) 
                        putInNodes.Add(child.Id);
                    insertChildren(child);
                }
            }
            if (node.HasClass("heading_sections"))
            {
                if (node.Id != null && Regex.IsMatch(node.Id, @"^question_\d+$"))
                    questionSequence.insert(node.Id, putInNodes.ToArray());
            }
        }
    }

    public class DefinitionDependentDagSequence : RandomDagSequence
    {
        public static string DefinitionPattern = "/[^\"]+|\"([^\"]*)\"|'([^']*)'/g";
        public static int MinDefinitionLength = 3;
        /// <summary>
        /// the Definiton string (given by <see cref="findDefSubstring"/>) -> the originals id: Manages the dependencies
        /// </summary>
        protected Dictionary<string, string> DefinitionDependencies = new Dictionary<string, string>();

        public DefinitionDependentDagSequence(string questionsMarkdown) 
            : base(questionsMarkdown) { }

        public DefinitionDependentDagSequence(FileInfo currentQuestionsPath)
            : base(currentQuestionsPath) { }

        protected override void insertChildren(HtmlNode node)
        {
            HtmlNodeCollection children = node.ChildNodes;
            List<string> headingDependenciesNodes = new List<string>();
            foreach (HtmlNode child in children)
            {
                if (child.HasClass("question_box"))
                {
                    headingDependenciesNodes.Add(child.Id);
                    sequenceInsertWithDependencies(child);
                }
                if (child.HasClass("heading_sections"))
                {
                    if (child.Id != null && Regex.IsMatch(child.Id, @"^question_\d+$"))
                        headingDependenciesNodes.Add(child.Id);
                    insertChildren(child);
                }
            }
            if (node.HasClass("heading_sections"))
            {
                if (node.Id != null && Regex.IsMatch(node.Id, @"^question_\d+$"))
                    sequenceInsertWithDependencies(node, headingDependenciesNodes);
            }
        }

        /// <summary>
        /// First inserts question to the sequence with all of its dependencies and if there is def in the question, also adds the def to <see cref="DefinitionDependencies"/>
        /// </summary>
        /// <param name="question">the question node</param>
        /// <param name="already_found">optional already found dependencies list</param>
        private void sequenceInsertWithDependencies(HtmlNode question, List<string>? already_found = null)
        {
            // insert to Dag with found definitions as dependencies
            questionSequence.insert(question.Id, findDependencies(question, already_found));

            // create new definition
            string? newDef = findDefSubstring(question);
            if (newDef != null) {
                DefinitionDependencies.Add(newDef, question.Id);
            }
        }

        private string? findDefSubstring(HtmlNode question)
        {
            string text = question.InnerText.Replace("“", "\"").Replace("”", "\"");
            string pattern = DefinitionPattern;
            var matches = Regex.Matches(text, pattern);
            if (matches == null || matches.Count == 0) return null;
            string res = matches
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .First();

            if (res != null && res.StartsWith('\"') && res.Count() >= MinDefinitionLength + 2)
                return res.Trim('"');
            else
                return null;
        } 

        private string[] findDependencies(HtmlNode question, List<string>? already_found = null)
        {
            List<string> dependencies = (already_found != null) ? already_found : new List<string>();
            string text = question.InnerText;
            foreach (var def in DefinitionDependencies.Keys)
            {
                if (text.ToLower().Contains(def.ToLower()))
                {
                    dependencies.Add(DefinitionDependencies[def]);
                }
            }

            return dependencies.ToArray();
        }
    }
}
