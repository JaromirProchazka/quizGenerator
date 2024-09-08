﻿using QuizLogicalComponents.AbstractChain;
using QuizLogicalComponents.QuizCreationChain;
using QuizLogicalComponents.QuizStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.TopicStartingChain
{
    /// <summary>
    /// A step in the Quiz Starting chain.
    /// </summary>
    public abstract record class QuizStartingStep : ChainStep<QuizProduct>
    {
        //public new virtual QuizStartingStep? Next { get; protected set; }

        /// <summary>
        /// The Quiz Starting product of last Step. If none given, it is default initialized.
        /// </summary>
        //public new virtual QuizProduct? BetweenStep { get; set; } = null;
    }

    /// <summary>
    /// The product of the <see cref="QuizStartingStep"/> chain.
    /// </summary>
    public record class QuizProduct : ChainProduct
    {
        /// <summary>
        /// A path to a local file where the final quiz file is.
        /// </summary>
        public string? pathToQuiz { get; set; } = null;

        /// <summary>
        /// The Quiz State, from which the quiz will be generated.
        /// </summary>
        public QuizState? state = null;
    }
}