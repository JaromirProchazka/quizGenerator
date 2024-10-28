using FileManager;
using quizGenerator;
using QuizLogicalComponents.AbstractChain;
using TopicCreation.QuizCreationChain;
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

#nullable enable

namespace QGenerator.UIComponents
{
    public partial class ChooseSourceStep : ChainStepForm<TopicCreationStep, TopicProduct, ChainCreationBuilder>
    {
        Action finalize;

        public ChooseSourceStep(Action finalize)
        {
            InitializeComponent();
            this.finalize = finalize;
        }

        private void ChooseSourceStep_Load(object sender, EventArgs e)
        {
            ContinueBtn.Text = "Continue";
            PopulateListElement(notesChooseOptions);
        }

        private static void PopulateListElement(ListBox listToBePopulated)
        {
            listToBePopulated.Items.Add(ChooseLocalHtmlFileOption.GetLabel());
            listToBePopulated.Items.Add(ChooseNotionNotes.GetLabel());
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (notesChooseOptions.SelectedIndex == -1) throw new Exception("Choose an option in the list menu!");
                TopicCreationStep? thisStep = null;

                if (notesChooseOptions.SelectedItem == null) throw new Exception("The selected item in the list is null");
                string? selectedOption = (string)notesChooseOptions.SelectedItem;
                if (selectedOption == ChooseLocalHtmlFileOption.GetLabel())
                {
                    thisStep = new ChooseLocalHtmlFileOption(GetLocalFile, finalize);
                }
                else if (selectedOption == ChooseNotionNotes.GetLabel())
                {
                    Uri? outUri;
                    if (NotionLink.Text == "" || !Uri.TryCreate(NotionLink.Text, UriKind.Absolute, out outUri))
                        throw new Exception("Invalid Notion page Url! Make sure to copy the link to your PUBLIC Notion page correctly!");

                    thisStep = new ChooseNotionNotes(outUri, finalize);
                }
                else return;

                _ = Builder?.AddStep(thisStep);
                var res = Finalize();
            }
            catch (Exception ex)
            {
                Notification.Notice(ex.Message);
            }

            this.Close();
        }

        private FileStream? GetLocalFile()
        {
            if (ChooseLocalFile.ShowDialog() != DialogResult.OK) return null;

            string notesPath = ChooseLocalFile.FileName;
            if (!File.Exists(notesPath)) return null;
            return File.OpenRead(notesPath);
        }
    }
}
