﻿using QuizLogicalComponents.AbstractChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStarting.TopicStartingChain
{
    /// <summary>
    /// The last step of the Quiz starting Chain. Starts the quiz and return the <see cref="QuizProduct"/> instance.
    /// </summary>
    public sealed record class FinalizeTopicStartingChain :
        QuizStartingStep
    {
        /// <summary>
        /// The Next step is null.
        /// </summary>
        public override QuizStartingStep? Next { get => null; }

        /// <summary>
        /// Starts the quiz and return the product.
        /// </summary>
        /// <returns></returns>
        public override QuizProduct Step()
        {
            if (BetweenStep == null) BetweenStep = new QuizProduct();
            return BetweenStep;
        }
    }

    /// <summary>
    /// The first Step in the Quiz Starting chain.
    /// </summary>
    public record class StartStartingChain :
        QuizStartingStep
    {
        /// <summary>
        /// Default Initializes the running Product and returns it.
        /// </summary>
        /// <returns>The running Product</returns>
        public override QuizProduct Step()
        {
            BetweenStep = new QuizProduct();
            return BetweenStep;
        }
    }
}
