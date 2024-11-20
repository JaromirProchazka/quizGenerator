using QuizStarting.TopicStartingChain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuizGeneratorPresentation;


namespace QuizGeneratorPresentation.QuizStarting
{
    public partial class ChooseQuizBeginningFrom : ChainStepForm<QuizStartingStep, QuizProduct, ChainStartingBuilder>
    {
        string pathToQuiz;

        public ChooseQuizBeginningFrom(string pathToQuiz)
        {
            InitializeComponent();
            this.pathToQuiz = pathToQuiz;
        }


        private void ChooseQuizBeginningFrom_Load(object sender, EventArgs e)
        {
            this.Text = "Choose Quiz Starting State";
            ContinueBtn.Text = "Continue";
            ContinueBtn.Enabled = false;
            populateListElement(notesChooseOptions);
            #nullable disable
            notesChooseOptions.DoubleClick += new EventHandler(notesChooseOptions_DoubleClick);
            notesChooseOptions.SelectedIndexChanged += new EventHandler(notesChooseOptions_SelectedIndexChanged);
            #nullable enable
        }

        private void notesChooseOptions_DoubleClick(object sender, EventArgs e)
        {
            ContinueBtn_Click(sender, e);
        }

        private static void populateListElement(ListBox listToBePopulated)
        {
            listToBePopulated.Items.Add(ContinueQuizWhereLastEnded.GetLabel());
            listToBePopulated.Items.Add(StartNewQuizFromBeginning.GetLabel());
        }

        private void notesChooseOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notesChooseOptions.SelectedItem == null) return;
            if (notesChooseOptions.SelectedIndex == -1)
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
                QuizStartingStep thisStep;


                if (notesChooseOptions.SelectedItem == null) return;

                string selectedOption = (string)notesChooseOptions.SelectedItem;

                if (selectedOption == ContinueQuizWhereLastEnded.GetLabel())
                {
                    thisStep = new ContinueQuizWhereLastEnded(pathToQuiz);
                }
                else if (selectedOption == StartNewQuizFromBeginning.GetLabel())
                {
                    thisStep = new StartNewQuizFromBeginning(pathToQuiz);
                }
                else return;

                _ = Builder?.AddStep(thisStep);

                var res = Finalize();

                if (res != null && res.state != null) new questionsForm(res.state).Show();

                this.Close();
            }
            catch (Exception ex)
            {
                //var notice = new Notification(ex.Message);
                //notice.Show();
                MessageBox.Show(ex.Message);
            }
        }
    }
}