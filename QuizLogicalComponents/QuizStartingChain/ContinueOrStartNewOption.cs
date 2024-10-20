using QuizLogicalComponents.AbstractChain;
using QuizLogicalComponents.QuizStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.TopicStartingChain
{
    
    public abstract record class ContinueOrStartNewOption : 
        QuizStartingStep
    {
        /// <summary>
        /// The loaded state.
        /// </summary>
        protected QuizState state;

        /// <summary>
        /// The State manager.
        /// </summary>
        protected BaseStateSerializer<ResetAroundState> serializer;

        public ContinueOrStartNewOption(string questionsFilePath)
        {
            serializer = new BaseStateSerializer<ResetAroundState>(questionsFilePath);
            state = serializer.LoadState();
        }
    }

    /// <summary>
    /// The <see cref="QuizStartingStep"/> which represents an option to start the quiz from the saved state.
    /// </summary>
    /// <param name="questionsFilePath"></param>
    public record class ContinueQuizWhereLastEnded(string questionsFilePath) :
        ContinueOrStartNewOption(questionsFilePath)
    {
        internal override QuizProduct Step()
        {
            if (BetweenStep == null) BetweenStep = new QuizProduct();
            BetweenStep.state = this.state;
            return BetweenStep;
        }

        public new static string GetLabel() => "Continue Quiz Where Last Ended";
    }

    /// <summary>
    /// The <see cref="QuizStartingStep"/> which represents an option to start the quiz from the beginning. New State is created.
    /// </summary>
    /// <param name="questionsFilePath"></param>
    public record class StartNewQuizFromBeginning(string questionsFilePath) :
        ContinueOrStartNewOption(questionsFilePath)
    {
        internal override QuizProduct Step()
        {
            if (BetweenStep == null) BetweenStep = new QuizProduct();
            BetweenStep.state = new ResetAroundState(questionsFilePath);
            return BetweenStep;
        }

        public new static string GetLabel() => "Start New Quiz From Beginning";
    }
}
