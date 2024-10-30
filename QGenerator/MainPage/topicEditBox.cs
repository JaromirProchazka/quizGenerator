using QuizPersistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizGeneratorPresentation.MainPage
{
    public partial class topicEditBox : Form
    {
        ListBox mainPageListBox;
        HyperLink item;
        public string? currentTopicDirectoryPath;

        public topicEditBox(ListBox _listBox, HyperLink _item)
        {
            mainPageListBox = _listBox;
            item = _item;
            setTopicDirectoryPath();

            InitializeComponent();
            textBox1.Lines[0] = QuestionsFile.GetQuizName(currentTopicDirectoryPath);
        }

        public void setTopicDirectoryPath()
        {
            currentTopicDirectoryPath = Path.GetDirectoryName(item.LinkToQuestions);
        }

        private void renameBtn_Click(object sender, EventArgs e)
        {
            if (currentTopicDirectoryPath == null)
                throw new Exception("The current topic directory path wasn't set!");

            if (textBox1.Text.Length == 0) return;

            string newName = textBox1.Text;
            string? newPath = QuestionsFile.RenameTopic(currentTopicDirectoryPath, newName);

            if (newPath == null) return;

            item.LinkToQuestions = Path.Combine(newPath, QuestionsFile.questionsFileName);
            item.LinkLabel = newName;
            setTopicDirectoryPath();

            mainPage.updateList(mainPageListBox);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            var dirName = Path.GetDirectoryName(item.LinkToQuestions);
            if (dirName == null) return;
            string pathToTopicFolder = Path.GetFullPath(dirName);
            if (QuestionsFile.DeleteTopic(pathToTopicFolder))
            {
                mainPage.updateList(mainPageListBox);
                Hide();
            }
        }

        private void topicEditBox_Load(object sender, EventArgs e)
        {

        }
    }
}