using QuizPersistence;
using QuizPersistence.DataStructures;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStarting
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
        Dag questionSequence = new Dag();
        List<string> questionsObjects = new List<string>();

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

        private void insertChildren(HtmlNode node)
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
                    putInNodes.Add(child.Id);
                    insertChildren(child);
                }
            }
            if (node.HasClass("heading_sections"))
            {
                questionSequence.insert(node.Id, putInNodes.ToArray());
            }
        }

        private void puloutQuestionsObjects(string questionName)
        {
            int substringStart = questionName.IndexOf('[');
            int substringEnd = questionName.IndexOf("]");
            if (substringStart != -1 && substringEnd != -1)
            {

            }
        }
    }
}
