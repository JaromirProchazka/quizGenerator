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
using FileManager.NotesParsing;
using System.Xml.Linq;

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

        static string templateSourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileManagers", Path.GetDirectoryName(utilFolderPath));

        /// <summary>
        /// Used for determining, which html nodes in the input source nodes are the questions 
        /// </summary>
        public static QuestionNodeParams? questionNodeAnalyzer { get; private set; }
        public static void SetQuestionNodeAnalyzer(QuestionNodeParams value)
        {
            parser.questionNodeAnalyzer = value;
            questionNodeAnalyzer = value;
        }
        internal static NotesParser parser;

        static QuestionsFile()
        {
            parser = new NotesParser();
        }

        /// <summary>
        /// Creates new topic folder with questions file to be later opened as quiz.
        /// </summary>
        /// <param name="notesPath">File path to the source notes</param>
        /// <returns>Path to the Topic</returns>
        public static string CreateNewTopic(string notesPath)
        {
            HtmlDocument htmlFile = new HtmlDocument();
            htmlFile.Load(notesPath);

            string topicTitle = NotesParser.getHtmlTitle(htmlFile);
            string topicName = Path.GetFileNameWithoutExtension(notesPath);

            string name = getUniqueName((topicTitle.Any()) ? topicTitle : topicName);

            string topicFolderPath = Path.Combine(
                utilFolderPath,
                QuestionsFile.toTopicFileName(name)
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

        /// <summary>
        /// Renames the Topic in the Topics fiolder
        /// </summary>
        /// <param name="currentTopicDirectoryPath">Path to the topics directory</param>
        /// <param name="newName">The new Topic name</param>
        /// <returns>New path to the Topics folder with new name, if the <see cref="currentTopicDirectoryPath"/> is valid</returns>
        public static string? RenameTopic(string currentTopicDirectoryPath, string newName)
        {
            string newPath = Path.Combine(Path.GetDirectoryName(currentTopicDirectoryPath), QuestionsFile.toTopicFileName(newName));
            if (Directory.Exists(newPath))
            {
                string uniqueName = addCopyToName(newName);
                return RenameTopic(currentTopicDirectoryPath, uniqueName);
            }
            
            if (isTopicFolder(currentTopicDirectoryPath))
            {
                Directory.Move(
                    currentTopicDirectoryPath,
                    newPath
                );

                return newPath;
            }

            return null;
        }

        private static string addCopyToName(string name)
        {
            string[] nameSplit = name.Split('.');
            nameSplit[0] = nameSplit[0] + "_copy";
            return string.Join("", nameSplit);
        }

        private static string getUniqueName(string name)
        {
            if ( !isTopicFolder(Path.Combine(utilFolderPath, toTopicFileName(name)))) return name;
            return getUniqueName(addCopyToName(name));
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
            return parser.CreateQuestionsFileText(notesMarkdown);
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