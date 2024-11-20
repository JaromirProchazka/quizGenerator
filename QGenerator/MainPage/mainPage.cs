using QuizPersistence;
using System;
using System.Windows.Forms;
using QuizStarting.TopicStartingChain;
using TopicCreation.TopicCreationChain;

namespace QuizGeneratorPresentation.MainPage
{
    public partial class mainPage : Form
    {
        public mainPage()
        {
            InitializeComponent();
        }

        private void mainPage_Load(object sender, EventArgs e)
        {
            this.Text = "Quiz Generator";
            createBtn.Text = "Create New Topic";
            TopicsList.Text = "List Of Topics";
            TopicsList.ItemHeight = 50;
            #nullable disable
            TopicsList.DoubleClick += new EventHandler(TopicsList_DoubleClick);
            #nullable enable
            openBtn.Enabled = false;
            editBtn.Enabled = false;
            
            populateListElement(TopicsList);
        }

        private void listBoxLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TopicsList.SelectedItem == null) return;
            if (TopicsList.SelectedIndex == -1)
            {
                openBtn.Enabled = false;
                editBtn.Enabled = false;
                return;
            }

            openBtn.Enabled = true;
            editBtn.Enabled = true;

            HyperLink selectedItem = (HyperLink)TopicsList.SelectedItem;
            Topics.OpenQuizPage(selectedItem.LinkToQuestions);
        }

        private void TopicsList_DoubleClick(object sender, EventArgs e)
        {
            openBtn_Click(sender, e);
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
            if (TopicsList.SelectedItem == null) return;
            if (TopicsList.SelectedIndex == -1)
            {
                openBtn.Enabled = false;
                editBtn.Enabled = false;
            }

            openBtn.Enabled = true;
            editBtn.Enabled = true;
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