using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;

namespace QuizLogicalComponents.QuizCreationChain
{
    /// <summary>
    /// an @ITopicCreationStep, which simply returns the result ending the Chain. 
    /// </summary>
    public sealed record class FinalizeTopicCreationChain() : TopicCreationStep()
    {
        public override TopicCreationStep? Next { get => null; }

        internal override TopicProduct Step()
        {
            var topicPath = QuestionsFile.CreateNewTopic(BetweenStep.pathToSource);

            BetweenStep.pathToSource = Path.Combine(topicPath, QuestionsFile.notesFileName);
            BetweenStep.pathToQuiz = Path.Combine(topicPath, QuestionsFile.questionsFileName);
            
            return BetweenStep;
        }
    }

    /// <summary>
    /// an @ITopicCreationStep, which Starts the Chain. 
    /// </summary>
    public sealed record class StartTopicCreationChain() : TopicCreationStep()
    {
        internal override TopicProduct Step()
        {
            BetweenStep = new TopicProduct();
            return BetweenStep;
        }
    }
}
