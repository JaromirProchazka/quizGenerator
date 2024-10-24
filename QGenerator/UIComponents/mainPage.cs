using FileManager;
using QGenerator.UIComponents;
using System;
using System.Windows.Forms;
using QuizStarting.TopicStartingChain;
using TopicCreation.QuizCreationChain;
using QGenerator;

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
            TopicsList.Text = "List Of Topics";
            populateListElement(TopicsList);
        }

        private void listBoxLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TopicsList.SelectedIndex == -1) return;
            if (TopicsList.SelectedItem == null) return;
            
            HyperLink selectedItem = (HyperLink)TopicsList.SelectedItem;
            Topics.OpenQuizPage(selectedItem.LinkToQuestions);
        }

        private static void populateListElement(ListBox listToBePopulated)
        {
            HyperLink[] topics = Topics.getListOfTopics();
            foreach (HyperLink link in topics)
            {
                listToBePopulated.Items.Add(link);
            }

        }

        private void newTopicBtn_Click(object sender, EventArgs e)
        {
            var chain = ChainUiInit.GetCreationChain(onTopicCreationFinish);
            chain.Show();
        }

        private void onTopicCreationFinish()
        {
            updateList(TopicsList);
        }

        private void Topics_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            if (TopicsList.SelectedIndex != -1)
            {
                if (TopicsList.SelectedItem == null) return;
                HyperLink selectedItem = (HyperLink)TopicsList.SelectedItem;
                //Topics.OpenQuizPage(
                //    selectedItem.LinkToQuestions
                //);
                var chain = ChainUiInit.GetStartingChain(selectedItem.LinkToQuestions);
                chain.Show();
            }
        }

        public static void updateList(ListBox listToBeUpdated)
        {
            listToBeUpdated.Items.Clear();
            populateListElement(listToBeUpdated);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (TopicsList.SelectedIndex != -1)
            {
                if (TopicsList.SelectedItem == null) return;
                HyperLink selectedItem = (HyperLink)TopicsList.SelectedItem;
                new topicEditBox(TopicsList, selectedItem).Show();
            }
        }
    }
}