using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;
using QuizLogicalComponents.AbstractChain;

namespace TopicCreation.TopicCreationChain
{
    /// <summary>
    /// Simply returns the result ending the Chain. 
    /// </summary>
    /// <param name="FirstStep">The Starting step chain of the to be Disposed</param>
    public sealed record class FinalizeTopicCreationChain(TopicCreationStep FirstStep) :
       TopicCreationStep
    {
        public override TopicCreationStep? Next { get => null; }

        /// <summary>
        /// The Product of last Step. 
        /// </summary>
        public override TopicProduct? BetweenStep { get; set; } = null;

        public override TopicProduct Step()
        {
            if (BetweenStep == null) BetweenStep = new TopicProduct();
            if (BetweenStep.pathToSource == null) throw new ArgumentException("Source choosing must be run before the Finalizing step!");
            var topicPath = QuestionsFile.CreateNewTopic(BetweenStep.pathToSource);

            BetweenStep.pathToSource = Path.Combine(topicPath, QuestionsFile.notesFileName);
            BetweenStep.pathToQuiz = Path.Combine(topicPath, QuestionsFile.questionsFileName);

            if (BetweenStep.finalize != null) BetweenStep.finalize.Invoke(); // Updates the Topics list

            FirstStep.Dispose();
            return BetweenStep;
        }
    }

    /// <summary>
    /// Starts the Chain. 
    /// </summary>
    public sealed record class StartTopicCreationChain :
        TopicCreationStep
    {
        /// <summary>
        /// The Product of last Step. 
        /// </summary>
        public override TopicProduct? BetweenStep { get; set; } = null;

        /// <summary>
        /// Default Initializes the running Product and returns it.
        /// </summary>
        /// <returns>The running Product</returns>
        public override TopicProduct Step()
        {
            BetweenStep = new TopicProduct();
            return BetweenStep;
        }
    }

}
