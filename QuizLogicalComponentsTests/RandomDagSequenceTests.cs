using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizLogicalComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerTests1;
using QuizStarting;

namespace QuizLogicalComponents.Tests
{
    [TestClass()]
    public class SequenceOfQuestionsTests
    {
        [TestMethod()]
        public void buildQuestionDag_onlyBasicQuestions()
        {
            RandomDagSequence questions = new RandomDagSequence(QuestionsFileTests.tData("basicQuestions.html"));
            List<string> sequence = questions.getSequence();
            int checkCounter = 0;
            string dagS = "";
            foreach (string s in sequence)
            {
                dagS += s + " ";
            }

            if (sequence.Count != 3)
            {
                Assert.Fail($"More or Less questions than necessary (is {sequence.Count} but should be 3).");
            }
            foreach (string s in new string[] { "question_1", "question_2", "question_3" })
            {
                checkCounter += (sequence.Contains(s)) ? 1 : 0;
            }

            if (checkCounter != 3)
            {
                Assert.Fail($"Missing item, ({dagS})");
            }
        }

        [TestMethod()]
        public void buildQuestionDag_headingsQuestions()
        {
            RandomDagSequence questions = new RandomDagSequence(QuestionsFileTests.tData("shortHeadingQuestion.html"));
            List<string> sequence = questions.getSequence();
            string[] dagIds = new string[] { "question_1", "question_2", "question_3", "question_4" };
            if (sequence.Count != 4)
            {
                Assert.Fail($"The sequence must have 4 items, but has {sequence.Count}.");
            }
            foreach (string s in dagIds)
            {
                if (!sequence.Contains(s))
                {
                    Assert.Fail($"Question {s} is missing.");
                }
            }

            if (sequence.Last() == "question_4")
            {
                Assert.Fail("BAD ordering, last must not be 'question_4'.");
            }
            sequence.Remove("question_4");
            if (!sequence.SequenceEqual(new List<string>(new string[] { "question_3", "question_2", "question_1", })))
            {
                Assert.Fail($"BAD ordering, first three must be 'question_3, question_2, question_1' but us '{sequence[0]}, {sequence[1]}, {sequence[2]}'.");
            }
        }

        [TestMethod()]
        public void buildQuestionsDag_NoQuestions()
        {
            RandomDagSequence questions = new RandomDagSequence(QuestionsFileTests.tData("noQuestions.html"));
            List<string> sequence = questions.getSequence();

            Assert.IsTrue(sequence.Count == 0, $"Should be empty, but is {sequence}");
        }
    }
}