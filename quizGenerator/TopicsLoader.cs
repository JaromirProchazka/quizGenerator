using FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quizGenerator
{
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
            System.Windows.Forms.Application.Run(new mainPage());
        }

        [STAThread]
        public static void OpenQuizPage(string questionsFile)
        {
            new questionsForm(questionsFile).Show();
        }
    }
}
