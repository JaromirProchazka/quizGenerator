using QuizLogicalComponents.QuizCreationChain;
using QuizLogicalComponents.TopicStartingChain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quizGenerator;

namespace QGenerator.UIComponents
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
            ContinueBtn.Text = "Continue";
            populateListElement(notesChooseOptions);
        }

        private static void populateListElement(ListBox listToBePopulated)
        {
            listToBePopulated.Items.Add(ContinueQuizWhereLastEnded.GetLabel());
            listToBePopulated.Items.Add(StartNewQuizFromBeginning.GetLabel());
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            try
            {
                QuizStartingStep thisStep;

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

                Builder.AddStep(thisStep);
                var res = Finalize();
                if (res != null) new questionsForm(res.state).Show();

                this.Close();
            }
            catch (Exception ex)
            {
                var notice = new Notification(ex.Message);
                notice.Show();
            }
        }
    }
}
