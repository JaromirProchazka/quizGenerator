using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Security.Policy;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime;
using Microsoft.VisualBasic.FileIO;

namespace FileManager
{
    /// <summary>
    /// Singleton containing methods and constants, to manage the 'quizGenerator' backend. It follows this file structure:
    /// <code>
    /// main_script.exe
    /// .sources\
    ///         |---------- styles.css
    ///         |---------- script.js
    ///         |
    ///         |---------- *general topic folder\
    ///         |                   |---------- notes.html
    ///         |                   |---------- questions.html
    ///         |                   |---------- state.json
    ///         |
    ///         |---------- ... more topic folders\
    /// </code>
    /// </summary>
    public static class QuestionsFile
    {
        public static string utilFolderPath = @".\.sources\";
        public static string questionsFileName = "questions.html";
        public static string notesFileName = "notes.html";
        public static string stylesFileName = "styles.css";
        public static string scriptFileName = "script.js";
        public static string quizState = "state.json";
        public static string questionsScriptName = "questionsScript.js";

        static string baseMarkdown = "<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" /><title></title><script type=\"text/javascript\" language=\"javascript\" src=\".\\..\\" + scriptFileName + "\"></script></head><body></body><link rel=\"stylesheet\" href=\".\\..\\" + stylesFileName + "\" /></html>";
        static string questionTemplate = "<div class=\"question_box\" id=\"question_0\" style=\"display: none\"><h1 class=\"question_name\"></h1><div class=\"question_answer\" style=\"display: none\"></div></div>";
        static string headingTemplate = "<div class=\"heading_sections\"><h1 class=\"section_heading\" style=\"display: none\"></h1><div class=\"heading_section_contents\" style=\"display: none\"></div></div>";

        static string templateSourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileManagers", Path.GetDirectoryName(utilFolderPath));

        /// <summary>
        /// Creates new topic folder with questions file to be later opened as quiz.
        /// </summary>
        /// <returns>Path to the Topic</returns>
        public static string CreateNewTopic(string notesPath)
        {
            HtmlDocument htmlFile = new HtmlDocument();
            htmlFile.Load(notesPath);

            string topicTitle = getHtmlTitle(htmlFile);
            string topicName = Path.GetFileNameWithoutExtension(notesPath);
            string topicFolderPath = Path.Combine(
                utilFolderPath,
                QuestionsFile.toTopicFileName((topicTitle.Any()) ? topicTitle : topicName)
            );

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

            return topicFolderPath;
        }

        /// <summary>
        /// Deletes the whole topic folder
        /// </summary>
        /// <param name="notesPath">Path to the topics folder</param>
        /// <returns>If the folder was deleted</returns>
        public static bool DeleteTopic(string notesPath)
        {
            if (isTopicFolder(notesPath))
            {
                Directory.Delete(notesPath, true);
                return true;
            }

            return false;
        }

        public static string? RenameTopic(string currentTopicDirectoryPath, string newName)
        {
            if (isTopicFolder(currentTopicDirectoryPath))
            {
                string newPath = Path.Combine(Path.GetDirectoryName(currentTopicDirectoryPath), QuestionsFile.toTopicFileName(newName));
                Directory.Move(
                    currentTopicDirectoryPath,
                    newPath
                );

                return newPath;
            }

            return null;
        }

        /// <summary>
        /// Determines, if the given path leads to valid topics folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool isTopicFolder(string path)
        {
            string fullPath = Path.GetFullPath(path);
            string fullSourcesPath = Path.GetFullPath(QuestionsFile.utilFolderPath);
            string fullParentPath = Path.GetFullPath(Path.GetDirectoryName(fullPath));
            return Directory.Exists(path) &&
                    fullPath.Contains(fullSourcesPath) &&
                    fullSourcesPath.Contains(fullParentPath);
        }

        /// <summary>
        /// Initializes the sources folder for the Topics (Quizes data).
        /// </summary>
        /// <returns>Path to the created folder</returns>
        private static string createSourcesFolder()
        {
            var currentDir = Directory.GetCurrentDirectory();
            // Directory.CreateDirectory(utilFolderPath);
            var sourcesCopyer = new RecursiveFolderCopy(templateSourcesPath);
            sourcesCopyer.CopyTo(Directory.GetCurrentDirectory(), recursive: true);

            return Path.Combine(currentDir, Path.GetFileName(templateSourcesPath));
        }

        private static string GetDataFilePath()
        {
            if (Directory.Exists(utilFolderPath)) return utilFolderPath;

            return createSourcesFolder();
        }

        /// <summary>
        /// Gives a collection of Topics in History.
        /// </summary>
        /// <returns>an array of names</returns>
        public static string[] GetTopics()
        {
            return Directory.GetDirectories(GetDataFilePath());
        }

        public static string toTopicFileName(string title)
        {
            return title.Replace(" ", "_");
        }

        public static string GetMarkDown(string markdownPath)
        {
            if (!File.Exists(markdownPath)) throw new Exception("Markdow questions not found!");
            return File.ReadAllText(markdownPath);
        }

        /// <summary>
        /// Stores Quiz State data of a given Quiz.
        /// </summary>
        /// <param name="quizName">Quiz name</param>
        /// <param name="jsonStateData">JSON state data to store</param>
        /// <returns>success status</returns>
        public static bool SaveQuizState(string quizName, string jsonStateData)
        {
            try
            {
                string stateFilePath = Path.Combine(utilFolderPath, quizName, quizState);
                File.WriteAllText(stateFilePath, jsonStateData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Load Quiz State data of a given Quiz.
        /// </summary>
        /// <param name="quizName">Quiz name</param>
        /// <returns>JSON formatted State data</returns>
        public static LoadedStateToken LoadQuizStateData(string quizName)
        {
            string stateFilePath = Path.Combine(utilFolderPath, quizName, quizState);
            if (!File.Exists(stateFilePath)) 
                return new LoadedStateToken(LoadedState.NotFound, "");
            return new LoadedStateToken(LoadedState.Valid, File.ReadAllText(stateFilePath));
        }

        /// <summary>
        /// State of the loaded state data.
        /// </summary>
        public enum LoadedState { Valid, NotFound };
        /// <summary>
        /// Holds data from loaded state File and a status field.
        /// </summary>
        public record struct LoadedStateToken(LoadedState State, string Data) { }

        /// <summary>
        /// Given a path to quiz return it's name
        /// </summary>
        /// <param name="pathToQuiz">Name of the quiz</param>
        /// <returns></returns>
        public static string GetQuizName(string pathToQuiz)
        {
            var dirInfo = new DirectoryInfo(pathToQuiz);
            return dirInfo.Parent.Name;
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
            bool noQuestionInHeadingYet = true;

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
                HtmlNode currentHeadingAnswer = null;
                foreach (HtmlNode child in children)
                {
                    areInHeading = false;
                    if (child.NodeType != HtmlNodeType.Element)
                    {
                        continue;
                    }

                    if (child.Name == "header")
                    {
                        continue;
                    }

                    if (isHeading(child))
                    {
                        addHeading(child);
                        areInHeading = true;
                        noQuestionInHeadingYet = true;
                        currentHeadingAnswer = resultCurrentPosition.SelectSingleNode("//*[contains(@class, 'heading_section_contents')]");
                    }
                    else if (child.Name == "em")
                    {
                        noQuestionInHeadingYet = false;
                        addQuestion(child);
                    }

                    if (!areInHeading && child.Name != "em")
                    {
                        goThrowNodes(child);
                    }

                    if (currentParentHeadingRank != 0 && noQuestionInHeadingYet && !areInHeading && currentHeadingAnswer != null)
                    {
                        currentHeadingAnswer.AppendChild(child);
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
                        while (parentHeadingRank >= headingRank)
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
            while (!blockElements.Contains(parent.Name) && !isHeading(parent))
            {
                parent = parent.ParentNode;
            }
            return parent;
        }

        private static string getHtmlTitle(HtmlDocument htmlFile)
        {
            HtmlNode title = htmlFile.DocumentNode.SelectSingleNode("//title");
            if (title != null)
            {
                return title.InnerText;
            }
            return "";
        }
    }

    /// <summary>
    /// A class containing data necessary for linking Quizzes.
    /// </summary>
    public class HyperLink
    {
        /// <summary>
        /// The label, of the hyperling, what user sees before clicking
        /// </summary>
        public string LinkLabel;
        /// <summary>
        /// The actual link to the quiz.
        /// </summary>
        public string LinkToQuestions;

        public HyperLink(string label, string link)
        {
            LinkLabel = label;
            LinkToQuestions = link;
        }

        public override string ToString()
        {
            return LinkLabel.Replace("_", " ");
        }
    }
}