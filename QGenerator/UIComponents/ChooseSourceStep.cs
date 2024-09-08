using FileManager;
using quizGenerator;
using QuizLogicalComponents.AbstractChain;
using QuizLogicalComponents.QuizCreationChain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace QGenerator.UIComponents
{
    public partial class ChooseSourceStep : Form
    {
        StartTopicCreationChain TopicCreator = new StartTopicCreationChain();
        Action finalize;

        public ChooseSourceStep(Action finalize)
        {
            InitializeComponent();
            this.finalize = finalize;
        }

        private void ChooseSourceStep_Load(object sender, EventArgs e)
        {
            ContinueBtn.Text = "Continue";
            populateListElement(notesChooseOptions);
        }

        private static void populateListElement(ListBox listToBePopulated)
        {
            listToBePopulated.Items.Add(ChooseLocalHtmlFileOption.GetLabel());
            listToBePopulated.Items.Add(ChooseNotionNotes.GetLabel());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (notesChooseOptions.SelectedIndex == -1) throw new Exception("Choose an option in the list menu!");
                TopicCreationStep? thisStep = null;

                string selectedOption = (string)notesChooseOptions.SelectedItem;
                if (selectedOption == ChooseLocalHtmlFileOption.GetLabel())
                {
                    thisStep = new ChooseLocalHtmlFileOption(GetLocalFile);
                }
                else if (selectedOption == ChooseNotionNotes.GetLabel())
                {
                    Uri? outUri;
                    if (NotionLink.Text == "" || !Uri.TryCreate(NotionLink.Text, UriKind.Absolute, out outUri))
                        throw new Exception("Invalid Notion page Url! Make sure to copy the link to your PUBLIC Notion page correctly!");

                    thisStep = new ChooseNotionNotes(outUri);
                }
                else return;

                thisStep.SetNext(new FinalizeTopicCreationChain());
                TopicCreator.SetNext(thisStep);

                TopicProduct? res = null;
                res = (TopicProduct)TopicCreator.DoStep();

                Console.WriteLine(res);
            }
            catch (Exception ex)
            {
                var notice = new Notification(ex.Message);
                notice.Show();
            }

            this.Close();
        }

        public new void Close()
        {
            TopicCreator.Dispose();
            finalize.Invoke();
            base.Close();
        }

        private FileStream? GetLocalFile()
        {
            if (ChooseLocalFile.ShowDialog() != DialogResult.OK) return null;

            string notesPath = ChooseLocalFile.FileName;
            return File.OpenRead(notesPath);
        }
    }
}
