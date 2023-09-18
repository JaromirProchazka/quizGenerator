using FileManager;
using System;
using System.Windows.Forms;

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
            Topics.Text = "List Of Topics";
            populateListElement();
        }

        private void populateListElement()
        {
            HyperLink[] topics = FileManager.Topics.getListOfTopics();
            foreach (HyperLink link in topics)
            {
                Topics.Items.Add(link);
            }
        }

        private void newTopicBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string notesPath = openFileDialog1.FileName;
                QuestionsFile.CreateNewTopic(notesPath);

                // Updates list of topics
                Topics.Items.Clear();
                populateListElement();
            }
        }

        private void Topics_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            if (Topics.SelectedIndex != -1)
            {
                HyperLink selectedItem = (HyperLink)Topics.SelectedItem;
                FileManager.Topics.OpenQuizPage(
                    selectedItem.Link
                );
            }
        }
    }
}
