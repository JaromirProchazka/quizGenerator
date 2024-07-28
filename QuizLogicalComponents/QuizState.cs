using FileManager;
using System;
using System.IO;


namespace QuizLogicalComponents
{
    public abstract record class IQuizState 
    {
        /// <summary>
        /// Path to the questions file.
        /// </summary>
        public string CurrentQuestionsPath;
        /// <summary>
        /// Index into the @QuestionIds.
        /// </summary>
        public int QuestionIndex;

        /// <summary>
        /// Moves the previous in the sequence forward in the questions.
        /// </summary>
        /// <param name="distance">The number of spaces in sequence, by which the questions Id is moved forward.</param>
        public abstract void MovePreviousAnswearedForward(int distance);

        /// <summary>
        /// Gives the number of questions.
        /// </summary>
        /// <returns>Positive number</returns>
        public abstract int GetQuestionsCount();

        /// <summary>
        /// Get's the next questions Id without changing the state (Peek). If we are at the sequences end, It resets the sequence using the @ISequenceOfQuestions and resets state to the sequences beginning.
        /// </summary>
        /// <returns>Questions Id</returns>
        public abstract string GetNextQuestion();

        /// <summary>
        /// Sets State to the next question in the Sequence of questions
        /// </summary>
        public abstract void SetNextQuestion();

        /// <summary>
        /// Uses @ISequenceOfQuestions to create new sequence of questions and resets the state such that it points to the sequences beginning. 
        /// </summary>
        public abstract void ResetQuestions();

        /// <summary>
        /// Gets the previous questions id without changing the state (Peek). If we are at the sequences start, it returns the first in the sequence.
        /// </summary>
        /// <returns>Questions Id</returns>
        public abstract string GetPreviousQuestion();

        /// <summary>
        /// Gets Question Id, the state currentaly points to
        /// </summary>
        /// <returns>Questions Id</returns>
        public abstract string GetCurrentQuestion();
    }

    /// <summary>
    /// Represents the state of the quiz in a given time.
    /// </summary>
    public record class QuizState : IQuizState
    {
        /// <summary>
        /// List of question Id, by which the questions are identified.
        /// </summary>
        public List<string> QuestionIds;

        /// <summary>
        /// Used for generating random sequence.
        /// </summary>
        private ISequenceOfQuestions sequenceFinder;

        /// <summary>
        /// Returns the random sequence generator.
        /// </summary>
        /// <returns></returns>
        public ISequenceOfQuestions GetSequence() => sequenceFinder;

        public QuizState(string currentQuestionsPath, ISequenceOfQuestions sequence)
        {
            sequenceFinder = sequence;
            CurrentQuestionsPath = currentQuestionsPath;
            QuestionIds = sequence.getSequence();

            if (QuestionIds.Count != 0) QuestionIndex = 0;
        }

        /// <summary>
        /// Uses @RandomDagSequence Sequence generator as default.
        /// </summary>
        /// <param name="currentQuestionsPath">Path to the notes</param>
        public QuizState(string currentQuestionsPath) : 
            this(
                currentQuestionsPath, 
                new RandomDagSequence(QuestionsFile.GetMarkDown(currentQuestionsPath))
            ) { }

        public override string GetCurrentQuestion() {
            return QuestionIds[QuestionIndex]; 
        }

        public override void SetNextQuestion() {
            QuestionIndex++;
        }

        public override string GetNextQuestion()
        {
            if (QuestionIndex == GetQuestionsCount() - 1)
            {
                ResetQuestions();
                return GetCurrentQuestion();
            }

            return QuestionIds[QuestionIndex + 1];
        }

        public override string GetPreviousQuestion()
        {
            if (QuestionIndex == 0) return QuestionIds[0];
            
            return QuestionIds[QuestionIndex - 1];
        }

        public override void MovePreviousAnswearedForward(int distance)
        {
            string previousQuestionId = GetPreviousQuestion();
            QuestionIds.RemoveAt(QuestionIndex - 1);
            QuestionIds.Insert(
                (QuestionIndex + distance < GetQuestionsCount()) ? QuestionIndex + distance : QuestionIds.Count,
                previousQuestionId
            );
            QuestionIndex--;
        }

        public override void ResetQuestions()
        {
            QuestionIds = sequenceFinder.getSequence();
            QuestionIndex = 0;
        }

        public override int GetQuestionsCount() => QuestionIds.Count;
    }
}