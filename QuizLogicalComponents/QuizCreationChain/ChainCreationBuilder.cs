using QuizLogicalComponents.AbstractChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.QuizCreationChain
{
    public record class ChainCreationBuilder : 
        ChainBuilder<TopicCreationStep, TopicProduct>
    {
        /// <summary>
        /// Initializes the Starting step.
        /// </summary>
        public ChainCreationBuilder()
        {
            Start = new StartTopicCreationChain();
            End = Start;
        }

        /// <summary>
        /// Adds the <see cref="FinalizeTopicCreationChain"/> to the end of the chain and returns the Chains start, from which user can execute the chain.
        /// </summary>
        /// <returns>Start of the Chain.</returns>
        public override ChainStep<TopicProduct> Build()
        {
            _ = AddStep(new FinalizeTopicCreationChain());
            return Start;
        }
    }
}
