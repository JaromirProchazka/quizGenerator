using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager;
using quizGenerator;
using System.IO;

namespace quizGenerator
{
    public partial class mainPage : Form
    {
        public mainPage()
        {
            InitializeComponent();
        }

        private void mainPage_Load(object sender, EventArgs e)
        {
            button1.Text = "Create New Topic";
            listBox1.Text = "List Of Topics";
            populateListElement();
        }

        private void listBoxLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                HyperLink selectedItem = (HyperLink)listBox1.SelectedItem;
                Topics.OpenQuizPage(selectedItem.Link);
            }
        }

        private void populateListElement()
        {
            HyperLink[] topics = Topics.getListOfTopics();
            foreach (HyperLink link in topics)
            {
                listBox1.Items.Add(link);
            }
            
        }

        private void newTopicBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string notesPath = openFileDialog1.FileName;
                QuestionsFile.CreateNewTopic(notesPath);

                // Updates list of topics
                listBox1.Items.Clear();
                populateListElement();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                HyperLink selectedItem = (HyperLink)listBox1.SelectedItem;
                Console.WriteLine(selectedItem.Link);
                Topics.OpenQuizPage(
                    selectedItem.Link
                );
            }
        }
    }
}
