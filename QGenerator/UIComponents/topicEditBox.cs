using FileManager;
using quizGenerator;
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

namespace quizGenerator
{
    public partial class topicEditBox : Form
    {
        ListBox mainPageListBox;
        HyperLink item;
        public string currentTopicDirectoryPath;

        public topicEditBox(ListBox _listBox, HyperLink _item)
        {
            this.mainPageListBox = _listBox;
            this.item = _item;
            setTopicDirectoryPath();

            InitializeComponent();
        }

        public void setTopicDirectoryPath()
        {
            currentTopicDirectoryPath = Path.GetDirectoryName(item.LinkToQuestions);
        }

        private void renameBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && isTopicFolder(currentTopicDirectoryPath))
            {
                string newPath = Path.Combine(Path.GetDirectoryName(currentTopicDirectoryPath), QuestionsFile.toTopicFileName(textBox1.Text));
                Directory.Move(
                    currentTopicDirectoryPath,
                    newPath
                );
                item.LinkToQuestions = Path.Combine(newPath, QuestionsFile.questionsFileName);
                item.LinkLabel = textBox1.Text;
                setTopicDirectoryPath();

                mainPage.updateList(mainPageListBox);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (isTopicFolder(currentTopicDirectoryPath))
            {
                File.Delete(item.LinkToQuestions);
                File.Delete(Path.Combine(currentTopicDirectoryPath, QuestionsFile.notesFileName));

                Directory.Delete(Path.GetFullPath(Path.GetDirectoryName(item.LinkToQuestions)));
                mainPage.updateList(mainPageListBox);
                this.Hide();
            }
        }

        private static bool isTopicFolder(string path)
        {
            string fullPath = Path.GetFullPath(path);
            string fullSourcesPath = Path.GetFullPath(QuestionsFile.utilFolderPath);
            string fullParentPath = Path.GetFullPath(Path.GetDirectoryName(fullPath));
            return Directory.Exists(path) && 
                    fullPath.Contains(fullSourcesPath) && 
                    fullSourcesPath.Contains(fullParentPath);
        }

        private void topicEditBox_Load(object sender, EventArgs e)
        {

        }
    }
}