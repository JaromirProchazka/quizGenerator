using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;
using QuizLogicalComponents.AbstractChain;

namespace QuizLogicalComponents.QuizCreationChain
{
    /// <summary>
    /// Simply returns the result ending the Chain. 
    /// </summary>
    public sealed record class FinalizeTopicCreationChain :
       TopicCreationStep
    {
        public override TopicCreationStep? Next { get => null; }

        /// <summary>
        /// The Product of last Step. 
        /// </summary>
        public new TopicProduct? BetweenStep = null;

        internal override TopicProduct Step()
        {
            var topicPath = QuestionsFile.CreateNewTopic(BetweenStep.pathToSource);

            BetweenStep.pathToSource = Path.Combine(topicPath, QuestionsFile.notesFileName);
            BetweenStep.pathToQuiz = Path.Combine(topicPath, QuestionsFile.questionsFileName);

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
        public new TopicProduct? BetweenStep = null;

        /// <summary>
        /// Default Initializes the running Product and returns it.
        /// </summary>
        /// <returns>The running Product</returns>
        internal override TopicProduct Step()
        {
            BetweenStep = new TopicProduct();
            return BetweenStep;
        }
    }

}
