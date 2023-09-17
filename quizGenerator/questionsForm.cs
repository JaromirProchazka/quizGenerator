using FileManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace quizGenerator
{
    /// <summary>
    /// An interface for specific quiz. A Path to the Questions is provided in constructor.
    /// </summary>
    public partial class questionsForm : Form
    {
        string currentQuestionsPath;
        sequenceOfQuestions sequenceFinder;
        List<string> questionIds;
        int questionIndex;

        public static int movingDistaneceOnBadAnswear = 10;

        /// <summary>
        /// Initiates the Form of a quiz.
        /// </summary>
        /// <param name="questionsFilePath">The path to the questions file in the ".sources" folder.</param>
        public questionsForm(string questionsFilePath)
        {
            currentQuestionsPath = questionsFilePath;
            InitializeComponent();
            sequenceFinder = new sequenceOfQuestions(File.ReadAllText(currentQuestionsPath));
            questionIds = sequenceFinder.getSequence();
            if (questionIds.Count != 0) 
            {
                questionIndex = 0;
            }
        }

        private void questionsForm_Load(object sender, EventArgs e)
        {
            string uriPath = @"file:///" + Path.GetFullPath(currentQuestionsPath).Replace(@"\", "/").Replace("#", "%23");
            webBrowser2.Url = new Uri(uriPath);
            // webBrowser2.Navigate(new Uri(uriPath));
            Console.WriteLine(uriPath);

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
            if (questionIndex != null)
            {
                showCurrentQuestion();
            }
        }

        private void webBrowser2_Navigating (object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = true;
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
            movePreviousAnswearedForward(movingDistaneceOnBadAnswear);
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
            textBox1.Text = questionIndex + " / " + questionIds.Count;
            if (questionIndex == questionIds.Count)
            {
                Stylings.maxScoreStyle(textBox1);
            }
        }


        // UTILS ====================================================================================================================================================

        private void NextQuestion()
        {
            if (questionIndex < questionIds.Count)
            {
                hideCurrentQuestion();
            }
            questionIndex++;
            if (questionIndex > questionIds.Count) 
            {
                questionIds = sequenceFinder.getSequence();
                questionIndex = 0;
            } if (questionIndex == questionIds.Count)
            {
                updateScore();
                questionIndex++;
            } else {
                showCurrentQuestion();
            }
        }

        private void showCurrentQuestion()
        {
            webBrowser2
            .Document
            .InvokeScript(
                "ShowQuestion",
                new object[] { questionIds[questionIndex] }
            );

            updateScore();
        }

        private void hideCurrentQuestion()
        {
            webBrowser2
            .Document
            .InvokeScript(
                "HideQuestion",
                new object[] { questionIds[questionIndex] }
            );
        }
        private void showAnswear()
        {
            if (questionIndex >= 0 && questionIndex < questionIds.Count)
            {
                webBrowser2
                .Document
                .InvokeScript(
                    "ShowAnswear",
                    new object[] { questionIds[questionIndex] }
                );
            }
        }

        private void movePreviousAnswearedForward(int distance)
        {
            string previousQuestionId = questionIds[questionIndex-1];
            questionIds.RemoveAt(questionIndex-1);
            questionIds.Insert(
                (questionIndex + distance < questionIds.Count) ? questionIndex + distance : questionIds.Count,
                previousQuestionId
            );
            questionIndex--;
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
