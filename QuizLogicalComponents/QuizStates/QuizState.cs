using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;


namespace QuizLogicalComponents.QuizStates
{
    public abstract record class IQuizState
    {
        /// <summary>
        /// Path to the questions file (Json Included on Serialization).
        /// </summary>
        [JsonInclude]
        public string CurrentQuestionsPath;

        /// <summary>
        /// Current questions ID (Json Included on Serialization).
        /// </summary>
        [JsonInclude]
        public int QuestionIndex;

        /// <summary>
        /// Says, if the score is in the Won state (Json Included on Serialization). 
        /// Default is false.
        /// </summary>
        [JsonInclude]
        public bool ScoreWonState { get; private set; } = false;

        public void SetScoreAsWinning() { ScoreWonState = true; }

        /// <summary>
        /// Moves the previous in the sequence forward in the questions.
        /// </summary>
        /// <param name="distance">The number of spaces in sequence, by which the questions Id is moved forward.</param>
        public abstract void MovePreviousAnswearedForward(int distance);

        /// <summary>
        /// Initialises new State instance.
        /// </summary>
        /// <param name="path">Path to the quiz</param>
        /// <returns></returns>
        public abstract IQuizState NewState(string path);

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

        /// <summary>
        /// After @JsonSerializer.Deserialize, it initializes the field, that were not Serialized.
        /// </summary>
        public virtual IQuizState InitUntrackedFields() { return this; }
    }

    /// <summary>
    /// Represents the state of the quiz in a given time.
    /// </summary>
    public record class ResetAroundState() : IQuizState
    {
        /// <summary>
        /// List of question Id, by which the questions are identified (Json Included on Serialization).
        /// </summary>
        [JsonInclude]
        public List<string> QuestionIds = new List<string>();

        /// <summary>
        /// Used for generating random sequence.
        /// </summary>
        [JsonIgnore]
        private ISequenceOfQuestions sequenceFinder;

        /// <summary>
        /// Returns the random sequence generator (Untracked by Json Serializer).
        /// </summary>
        /// <returns></returns>
        public ISequenceOfQuestions GetSequence() => sequenceFinder;

        public ResetAroundState(string currentQuestionsPath, ISequenceOfQuestions sequence) : this()
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
        public ResetAroundState(string currentQuestionsPath) :
            this(
                currentQuestionsPath,
                new RandomDagSequence(new FileInfo(currentQuestionsPath))
            )
        { }

        public override IQuizState NewState(string path) => new ResetAroundState(path);

        public override string GetCurrentQuestion()
        {
            if (QuestionIndex >= GetQuestionsCount() || QuestionIndex < 0 ) QuestionIndex = 0;
            return QuestionIds[QuestionIndex];
        }

        public override void SetNextQuestion()
        {
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
                QuestionIndex + distance < GetQuestionsCount() ? QuestionIndex + distance : QuestionIds.Count,
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

        public override ResetAroundState InitUntrackedFields()
        {
            sequenceFinder = new RandomDagSequence(new FileInfo(CurrentQuestionsPath));
            return this;
        }
    }
}