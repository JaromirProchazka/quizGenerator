using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Security.Policy;
using FileManager;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using quizGenerator;
using System.Threading;

namespace FileManager
{
    /// <summary>
    /// A class used to get a random sequence of questions. 
    /// The questions implemented as HTML ID strings, which refere to the questions elements in the markdown provided in constructor.
    /// <example>
    /// <code>
    /// FileManager.sequenceOfQuestions questions = new FileManager.sequenceOfQuestions(File.ReadAllText(path));
    /// var listOfRandomQuestions = questions.getSequence();
    /// </code>
    /// </example>
    /// </summary>
    public class sequenceOfQuestions
    {
        Dag questionSequence = new Dag();
        List<string> questionsObjects = new List<string>();

        public sequenceOfQuestions(string questionsMarkdown)
        {
            HtmlDocument questions = new HtmlDocument();
            questions.LoadHtml(questionsMarkdown);
            insertChildren(questions.DocumentNode.
                SelectSingleNode("//body")
            );
        }

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

    /// <summary>
    /// Containes methods and constants, to manage the 'quizGenerator' backend. It follows this file structure:
    /// <code>
    /// main_script.exe
    /// .sources\
    ///         |---------- styles.css
    ///         |
    ///         |---------- *general topic folder\
    ///         |                   |---------- notes.html
    ///         |                   |---------- questions.html
    ///         |
    ///         |---------- ... more topic folders\
    /// </code>
    /// </summary>
    public class QuestionsFile
    {
        public static string utilFolderPath = @".\.sources\";
        public static string questionsFileName = "questions.html";
        public static string notesFileName = "notes.html";
        public static string stylesFileName = "styles.css";
        public static string questionsScriptName = "questionsScript.js";

        static string baseMarkdown = "<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" /><title></title><script type=\"text/javascript\" language=\"javascript\"> function ShowAnswear(id) { n = document.querySelector(\"#\" + id); if (n.classList.contains(\"heading_sections\")) { qs = n.querySelectorAll(\".question_box\"); for (let i = 0; i < qs.length; i++) { qs[i].style.display = \"block\"; qs[i].querySelector(\".question_answer\").style.display = \"block\"; } } else { n.querySelector(\".question_answer\").style.display = \"block\"; } } function ShowQuestion(id) { n = document.querySelector(\"#\" + id); if (n.classList.contains(\"heading_sections\")) { n = n.querySelector(\".section_heading\"); } n.style.display = \"block\"; } function HideQuestion(id) { n = document.querySelector(\"#\" + id); if (n.classList.contains(\"heading_sections\")) { qs = n.querySelectorAll(\".question_box\"); for (let i = 0; i < qs.length; i++) { qs[i].style.display = \"none\"; qs[i].querySelector(\".question_answer\").style.display = \"none\"; } n = n.querySelector(\".section_heading\"); } else { n.querySelector(\".question_answer\").style.display = \"none\"; console.log(\"problem\"); } n.style.display = \"none\"; }</script></head><body></body><link rel=\"stylesheet\" href=\".\\..\\" + stylesFileName + "\" /></html>";
        static string questionTemplate = "<div class=\"question_box\" id=\"question_0\" style=\"display: none\"><h1 class=\"question_name\"></h1><div class=\"question_answer\" style=\"display: none\"></div></div>";
        static string headingTemplate = "<div class=\"heading_sections\"><h1 class=\"section_heading\" style=\"display: none\"></h1></div>";


        /// <summary>
        /// Creates new topic folder with questions file to be later opened as quiz.
        /// </summary>
        public static void CreateNewTopic(string notesPath)
        {
            HtmlDocument htmlFile = new HtmlDocument();
            htmlFile.Load(notesPath);

            string topicTitle = getHtmlTitle(htmlFile).Replace(" ", "_");
            string topicName = Path.GetFileNameWithoutExtension(notesPath).Replace(" ", "_");
            string topicFolderPath = Path.Combine(utilFolderPath, (topicTitle.Any()) ? topicTitle : topicName);

            if (Directory.Exists(topicFolderPath)) 
            {
                File.WriteAllText(Path.Combine(topicFolderPath, notesFileName), topicTitle);
            }
            Directory.CreateDirectory(topicFolderPath);

            string notesMarkdown = htmlFile.DocumentNode.OuterHtml;
            File.WriteAllText(
                Path.Combine(topicFolderPath, notesFileName),
                notesMarkdown
            );
            File.WriteAllText(
                Path.Combine(topicFolderPath, questionsFileName),
                CreateQuestionsFileText(notesMarkdown)
            );
        }

        /// <summary>
        /// Takes a html file with notes, and creates a corresponding html markdown contents of just the quiz parts of the notes in standartised format.
        /// Also uses a config file for the specifics of notes creation.
        /// <example>
        /// <code>
        /// questionsMarkdown = FileManager.QuestionsFile.createQuestionsFile(
        ///         System.IO.File.ReadAllText("..\\FileManagerTests\\src\\basicNotes.html")
        /// );
        /// System.IO.File.WriteAllText(resultsFilePath, questionsMarkdown);
        /// </code>
        /// </example>
        /// </summary>
        public static string CreateQuestionsFileText(string notesMarkdown)
        {
            int idCounter = 1;
            int currentParentHeadingRank = 0;

            HtmlDocument notes = new HtmlDocument();
            HtmlDocument result = new HtmlDocument();
            notes.LoadHtml(notesMarkdown);
            result.LoadHtml(baseMarkdown);
            HtmlNode resultCurrentPosition = result.DocumentNode.SelectSingleNode("//body");

            HtmlNode bodyNode = notes.DocumentNode.SelectSingleNode("//body");
            goThrowNodes(bodyNode);

            return result.DocumentNode.OuterHtml;


            void goThrowNodes(HtmlNode node)
            {
                HtmlNodeCollection children = node.ChildNodes;
                bool areInHeading = false;
                foreach (HtmlNode child in children)
                {
                    areInHeading = false;
                    if (child.NodeType != HtmlNodeType.Element)
                    {
                        continue;
                    }

                    if (child.Name == "header") {
                        continue;
                    }
                    if (isHeading(child)) {
                        addHeading(child);
                        areInHeading = true;
                    }
                    if (child.Name == "em") {
                        addQuestion(child);
                    }
                    if (!areInHeading) {
                        goThrowNodes(child);
                    }
                }

                void addQuestion(HtmlNode question)
                {
                    if (question == null) { return; }
                    HtmlDocument questionBoxTemplate = new HtmlDocument();
                    questionBoxTemplate.LoadHtml(questionTemplate);
                    addIdCounterToNodeInDoc(questionBoxTemplate);

                    HtmlNode latestQuestionBox = questionBoxTemplate
                        .DocumentNode
                        .SelectSingleNode("//*[contains(@class, 'question_box')]");
                    HtmlNode questionBox =  resultCurrentPosition
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

                void addHeading(HtmlNode heading)
                {
                    if (heading == null) { return; }
                    HtmlDocument headingBoxTemplate = new HtmlDocument();
                    headingBoxTemplate.LoadHtml(headingTemplate);
                    addIdCounterToNodeInDoc(headingBoxTemplate);
                    int headingRank = getHeadingRank(heading);

                    if (headingRank <= currentParentHeadingRank) 
                    {
                        HtmlNode currentParentHeading = resultCurrentPosition.ParentNode;
                        int parentHeadingRank = currentParentHeadingRank - 1;
                        while(parentHeadingRank >= headingRank)
                        {
                            currentParentHeading = currentParentHeading.ParentNode;
                            parentHeadingRank--;
                        }
                        resultCurrentPosition = currentParentHeading;
                    }
                    HtmlNode headingBox = resultCurrentPosition.AppendChild(headingBoxTemplate.DocumentNode.SelectSingleNode("//div"));
                    resultCurrentPosition = headingBox.SelectSingleNode("//*[contains(@class, 'heading_sections')]");
                    currentParentHeadingRank = headingRank;

                    headingBox.SelectSingleNode("//*[contains(@class, 'section_heading')]").InnerHtml = heading.InnerText;
                }

                void addIdCounterToNodeInDoc(HtmlDocument docForId)
                {
                    docForId.DocumentNode
                        .SelectSingleNode("//div")
                        .SetAttributeValue("id", $"question_{idCounter}");
                    idCounter++;
                }
            }
        }

        /// <summary>
        /// Takes a HtmlNode and if it is a heading like 'h3', returns it's rank, in our situation '3' or it returns '0', if it isn't a heading node.
        /// </summary>
        private static int getHeadingRank(HtmlNode hx)
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

        private static bool isHeading(HtmlNode node)
        {
            return getHeadingRank(node) > 0;
        }

        private static HtmlNode ParentTextNode(HtmlNode keySegment)
        {
            string[] blockElements = { "p", "ul", "ol", "pre", "div", "blockquote", "dl", "figure", "hr" };
            HtmlNode parent = keySegment;
            while (!blockElements.Contains(parent.Name) && !isHeading(parent)) {
                parent = parent.ParentNode;
            }
            return parent;
        }

        private static string getHtmlTitle(HtmlDocument htmlFile)
        {
            HtmlNode title = htmlFile.DocumentNode.SelectSingleNode("//title");
            if (title != null) {
                return title.InnerText;
            }
            return "";
        }
    }

    public static class Topics
    {
        /// <summary>
        /// In the currrent directory tries to find "utilFolderPath" folder and returns array of all topic names.
        /// </summary>
        /// <returns></returns>
        public static HyperLink[] getListOfTopics()
        {
            string[] topics = Directory.GetDirectories(QuestionsFile.utilFolderPath);
            HyperLink[] links = new HyperLink[topics.Length];
            
            for (int i = 0; i < topics.Length; i++)
            {
                links[i] = new HyperLink(Path.GetFileNameWithoutExtension(topics[i]), getLinkForTopic(topics[i]));
            }

            return links;
        }

        private static string getLinkForTopic(string topicLabel)
        {
            return Path.Combine(topicLabel, "questions.html");
        }

        public static void OpenMainPage()
        {
            const int IE11EmulationMode = 11001;
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", appName, IE11EmulationMode, Microsoft.Win32.RegistryValueKind.DWord);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new mainPage());
        }

        [STAThread]
        public static void OpenQuizPage(string questionsFile)
        {
            new questionsForm(questionsFile).Show();
        }
    }

    public class HyperLink
    {
        public string LinkLabel;
        public string Link;

        public HyperLink(string label, string link)
        {
            LinkLabel = label;
            Link = link;
        }

        public override string ToString()
        {
            return LinkLabel.Replace("_", " ");
        }
    }
}