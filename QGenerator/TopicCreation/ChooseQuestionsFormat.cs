using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopicCreation.TopicCreationChain;
using NotesParsing;
using System.CodeDom;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace QuizGeneratorPresentation.TopicCreation
{
    public partial class ChooseQuestionsFormat : //Form 
        ChainStepForm<TopicCreationStep, TopicProduct, ChainCreationBuilder>
    {
        public ChooseQuestionFormat.ChecksLogicalOperator LogicalOperator = ChooseQuestionFormat.ChecksLogicalOperator.AND;
        public string? name = "em";
        enum NameOption { em = 0, strong, h1, h2, h3, a, ul, li, dl }
        public string? classes = null;
        public string? color = null;
        public string? font = null;


        public ChooseQuestionsFormat()
        {
            InitializeComponent();
            this.Text = "Choose Questions Format From Your Notes";
            DescriptionLabel.Text = "Choose the styling of questions in Notes. The node elements in your notes that has the attributes you\nset bellow will be interpreted as questions.";
            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        /// <summary>
        /// The OR option
        /// </summary>
        private void OrOption_CheckedChanged(object sender, EventArgs e)
        {
            LogicalOperator = ChooseQuestionFormat.ChecksLogicalOperator.OR;
        }

        /// <summary>
        /// The AND option
        /// </summary>
        private void AndOption_CheckedChanged(object sender, EventArgs e)
        {
            LogicalOperator = ChooseQuestionFormat.ChecksLogicalOperator.AND;
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var step = new ChooseQuestionFormat(LogicalOperator, name, classes, color, font);
                _ = Builder?.AddStep(step);
                _ = Finalize();
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

        private void nameInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            NameOption index = (NameOption)comboBox1.SelectedIndex;
            switch (index)
            {
                case NameOption.em:
                    name = "em";
                    break;
                case NameOption.strong:
                    name = "strong";
                    break;
                case NameOption.h1:
                    name = "h1";
                    break;
                case NameOption.h2:
                    name = "h2";
                    break;
                case NameOption.h3:
                    name = "h3";
                    break;
                case NameOption.a:
                    name = "a";
                    break;
                case NameOption.ul:
                    name = "ul";
                    break;
                case NameOption.li:
                    name = "li";
                    break;
                case NameOption.dl:
                    name = "dl";
                    break;
                default:
                    name = null;
                    break;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void colorInput_TextChanged(object sender, EventArgs e)
        {
            var input = textBox3.Text;
            if (input == "") return;
            color = input;
        }

        private void classesInput_TextChanged_1(object sender, EventArgs e)
        {
            var input = textBox1.Text;
            if (input == "") return;
            classes = input;
        }

        private void fontInput_TextChanged(object sender, EventArgs e)
        {
            var input = textBox2.Text;
            if (input == "") return;
            font = input;
        }
    }
}
