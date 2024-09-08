using QuizLogicalComponents.AbstractChain;
using QuizLogicalComponents.QuizCreationChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.TopicStartingChain
{
    public record class ChainStartingBuilder :
        ChainBuilder<QuizStartingStep, QuizProduct>
    {
        /// <summary>
        /// Initializes the Starting step.
        /// </summary>
        public ChainStartingBuilder()
        {
            Start = new StartStartingChain();
            End = Start;
        }

        /// <summary>
        /// Creates the Chian.
        /// </summary>
        /// <returns>returns the first step of the chain</returns>
        public override ChainStep<QuizProduct> Build()
        {
            _ = AddStep(new FinalizeTopicStartingChain());
            return Start;
        }
    }
}
