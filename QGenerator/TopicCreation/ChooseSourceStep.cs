using QuizPersistence;
using QuizGeneratorPresentation;
using QuizLogicalComponents.AbstractChain;
using TopicCreation.TopicCreationChain;
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
using System.Reflection.Emit;

#nullable enable

namespace QuizGeneratorPresentation.TopicCreation
{
    public partial class ChooseSourceStep : //Form 
        ChainStepForm<TopicCreationStep, TopicProduct, ChainCreationBuilder>
    {
        Action finalize;
        public string[] supportedFileFormatsExtensions = ["html"];

        public ChooseSourceStep(Action finalize)
        {
            InitializeComponent();
            this.finalize = finalize;
        }

        private void ChooseSourceStep_Load(object sender, EventArgs e)
        {
            this.Text = "Choose Quiz Notes Source";
            ContinueBtn.Text = "Continue";
            NotionLinkLabel.Text = "Notion page Url: Make sure that your page IS PUBLIC!";
            DescriptionLabel.Text = "Choose from where to take your notes and after that input them.";
            ContinueBtn.Enabled = false;

            NotionLink.Visible = false;
            NotionLinkLabel.Visible = false;

            PopulateListElement(notesChooseOptions);
            #nullable disable
            notesChooseOptions.DoubleClick += new EventHandler(notesChooseOptions_DoubleClick);
            notesChooseOptions.SelectedIndexChanged += new EventHandler(notesChooseOptions_SelectedIndexChanged);
            NotionLink.TextChanged += new EventHandler(ListBox1_SelectedIndexChanged);
            #nullable enable
        }

        private void notesChooseOptions_DoubleClick(object sender, EventArgs e)
        {
            if (notesChooseOptions.SelectedIndex == 1)
            {
                NotionLink.Visible = true;
                NotionLinkLabel.Visible = true;
                return;
            }

            NotionLink.Visible = false;
            NotionLinkLabel.Visible = false;
            ContinueBtn_Click(sender, e);
        }



        private void NotionLink_TextChange(object sender, EventArgs e)
        {

        }

        private void notesChooseOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notesChooseOptions.SelectedItem == null) return;
            if (notesChooseOptions.SelectedIndex == -1)
            {
                ContinueBtn.Enabled = false;
                return;
            }
            if (notesChooseOptions.SelectedIndex == 1)
            {
                NotionLink.Visible = true;
                NotionLinkLabel.Visible = true;
                return;
            }

            NotionLink.Visible = false;
            NotionLinkLabel.Visible = false;
            ContinueBtn.Enabled = true;
        }

        private static void PopulateListElement(ListBox listToBePopulated)
        {
            listToBePopulated.Items.Add(ChooseLocalHtmlFileOption.GetLabel());
            listToBePopulated.Items.Add(ChooseNotionNotes.GetLabel());
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NotionLink.Text == "")
            {
                ContinueBtn.Enabled = false;
                return;
            }

            ContinueBtn.Enabled = true;
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
            catch (FileLoadException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (HttpIOException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Console.Error.WriteLine(ex.ToString());
                MessageBox.Show("Something went wrong, please try again!");
                return;
            }

            this.Close();
        }

        /// <summary>
        /// Is responsible for fetching local file of correct format
        /// </summary>
        /// <returns>Maybe an opened file stream of chosen file</returns>
        /// <exception cref="FileNotFoundException">If the chosen file can't be found</exception>
        /// <exception cref="FileFormatException">If the chosen file has unsupported format</exception>
        /// <exception cref="ApplicationException">When the dialog couldn't be opened</exception>
        private FileStream GetLocalFile()
        {
            // OpenFileDialog Config
            var filter = "";
            for (int i = 0; i < supportedFileFormatsExtensions.Length; i++)
            {
                filter += $"Notes File (*.{supportedFileFormatsExtensions[i]})|*.{supportedFileFormatsExtensions[i]}";
                if (i != supportedFileFormatsExtensions.Length-1)
                {
                    filter += "|";
                }
            }
            ChooseLocalFile.Filter = filter;
            ChooseLocalFile.RestoreDirectory = true;

            // Provide file dialog
            if (ChooseLocalFile.ShowDialog() != DialogResult.OK) throw new ApplicationException("Notes file not Chosen!");

            if (supportedFileFormatsExtensions
                .Where((format) => $".{format}" == Path.GetExtension(ChooseLocalFile.FileName)).Count() == 0)
            {
                throw new FileFormatException($"The chosen file has unsupported format. Must be one of: {ChooseLocalFile.Filter.Replace("|", ",").Replace('*', ' ')}!");
            }

            string notesPath = ChooseLocalFile.FileName;

            if (!File.Exists(notesPath)) throw new FileNotFoundException("The given file wasn't found!");
            return File.OpenRead(notesPath);
        }
    }
}