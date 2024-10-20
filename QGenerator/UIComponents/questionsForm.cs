using FileManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuizLogicalComponents.QuizStates;

namespace quizGenerator
{
    /// <summary>
    /// An interface for specific quiz. A Path to the Questions is provided in constructor.
    /// </summary>
    public partial class questionsForm : Form
    {
        QuizState state;
        BaseStateSerializer<ResetAroundState> serializer;
        public static int movingDistanceOnBadAnswear = 10;

        /// <summary>
        /// Initiates the Form of a quiz from the start (from default). Uses the @ResetAroundState State class.
        /// </summary>
        /// <param name="questionsFilePath">The path to the questions file in the ".sources" folder.</param>
        public questionsForm(string questionsFilePath)
        {
            InitializeComponent();
            serializer = new BaseStateSerializer<ResetAroundState>(questionsFilePath);
            state = serializer.LoadState();
        }

        /// <summary>
        /// Initiates the Form of a quiz from a given State (history).
        /// </summary>
        /// <param name="givenState">The state of the quiz</param>
        public questionsForm(QuizState givenState)
        {
            InitializeComponent();
            if (givenState.CurrentQuestionsPath == null) throw new Exception("Quiz State was not set correctly!");
            serializer = new BaseStateSerializer<ResetAroundState>(givenState.CurrentQuestionsPath);
            state = givenState;
        }

        private void questionsForm_Load(object sender, EventArgs e)
        {
            if (state.CurrentQuestionsPath == null) throw new Exception("Quiz State was not set correctly!");
            string uriPath = @"file:///" + Path.GetFullPath(state.CurrentQuestionsPath).Replace(@"\", "/").Replace("#", "%23");
            webBrowser2.Url = new Uri(uriPath);

            button1.Text = "Next 👍";
            Stylings.goodButtonStyle(button1);

            button2.Text = "Next 👎";
            Stylings.badButtonStyle(button2);

            button3.Text = "Back";
            button4.Text = "Answer";
            Stylings.defaultButtonStyle(button3, button4);

            Stylings.scoreStyle(textBox1);
            updateScore();
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser2.Navigating += webBrowser2_Navigating;
            showCurrentQuestion();
        }

        private void webBrowser2_Navigating(object? sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = true;
            if (e.Url == null) return;
            System.Diagnostics.Process.Start(e.Url.ToString());
        }

        /// <summary>
        /// Hides the current question (and also the answear) and displays a new question.
        /// </summary>
        private void godNext_Click(object sender, EventArgs e)
        {
            NextQuestion();
        }

        /// <summary>
        /// Hides the current question (and also the answear) and displays a new question while also moving the wrongly answeared question forward, so that it can be asked again.
        /// </summary>
        private void badNext_Click(object sender, EventArgs e)
        {
            NextQuestion();
            state.MovePreviousAnsweredForward(movingDistanceOnBadAnswear);
            updateScore();
        }

        /// <summary>
        /// Shows the answear to the current question.
        /// </summary>
        private void AnswearButton_Click(object sender, EventArgs e)
        {
            showAnswear();
        }

        /// <summary>
        /// Return to the main page so that new Topic can be chosen, or created.
        /// </summary>
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateScore()
        {
            textBox1.Text = state.QuestionIndex + " / " + state.GetQuestionsCount();
            if (state.ScoreWonState || state.QuestionIndex == state.GetQuestionsCount())
            {
                Stylings.maxScoreStyle(textBox1);
                state.SetScoreAsWinning();
            }
        }


        // UTILS ====================================================================================================================================================

        private void NextQuestion()
        {
            if (state.QuestionIndex < state.GetQuestionsCount())
            {
                hideCurrentQuestion();
            }
            state.SetNextQuestion();

            if (state.QuestionIndex > state.GetQuestionsCount())
            {
                state.ResetQuestions();
            }

            if (state.QuestionIndex == state.GetQuestionsCount())
            {
                updateScore();
                state.SetNextQuestion();
            }
            else
            {
                showCurrentQuestion();
            }

            serializer.StoreState((ResetAroundState)state);
        }

        private void showCurrentQuestion()
        {
            webBrowser2
            .Document?
            .InvokeScript(
                "ShowQuestion",
                new object[] { state.GetCurrentQuestion() }
            );

            updateScore();
        }

        private void hideCurrentQuestion()
        {
            webBrowser2
            .Document?
            .InvokeScript(
                "HideQuestion",
                new object[] { state.GetCurrentQuestion() }
            );
        }
        private void showAnswear()
        {
            if (state.QuestionIndex >= 0 && state.QuestionIndex < state.GetQuestionsCount())
            {
                webBrowser2
                .Document?
                .InvokeScript(
                    "ShowAnswear",
                    new object[] { state.GetCurrentQuestion() }
                );
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }

    public static class Stylings 
    {
        private static Color badColor = Color.Red;
        private static Color goodColor = Color.Green;
        private static Color defaultColor = Color.Tomato;


        public static void goodButtonStyle(Button gButton)
        {
            gButton.BackColor = goodColor;
        }

        public static void badButtonStyle(Button gButton)
        {
            gButton.BackColor = badColor;
        }

        public static void defaultButtonStyle(params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.BackColor = defaultColor;
            }
        }

        public static void scoreStyle(TextBox t)
        {
            t.BackColor = Color.Tomato;
        }

        public static void  maxScoreStyle(TextBox t)
        {
            t.BackColor = goodColor;
        }
    }
}
