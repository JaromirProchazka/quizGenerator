using QuizLogicalComponents.QuizCreationChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.AbstractChain
{
    public abstract record class ChainBuilder<StepT, ProductT> 
        where ProductT : class, new()
        where StepT : ChainStep<ProductT>
    {
        /// <summary>
        /// The starting step in the chain.
        /// </summary>
        public ChainStep<ProductT> Start;

        /// <summary>
        /// The current end Step in the chain.
        /// </summary>
        public ChainStep<ProductT> End;

        /// <summary>
        /// Adds another step to the end of the chain.
        /// </summary>
        /// <param name="step">The next step instance.</param>
        /// <returns>This Builder for fluent syntax.</returns>
        public ChainBuilder<StepT,ProductT> AddStep(ChainStep<ProductT> step)
        {
            _ = End.SetNext(step);
            End = step;

            return this;
        }

        /// <summary>
        /// Creates new instance of the given StepType and adds it to the end of the Chain.
        /// </summary>
        /// <typeparam name="StepT">The type of the Creation step.</typeparam>
        /// <param name="args">The arguments to the Generic arguments constructor.</param>
        /// <returns></returns>
        public ChainBuilder<StepT,ProductT> AddStepT(params object[] args)
        {
            StepT step = (StepT)Activator.CreateInstance(typeof(StepT), args);
            return AddStep(step);
        }

        /// <summary>
        /// Adds the <see cref="IFinalizeChain{ProductT}"/> to the end of the chain and returns the Chains start, from which user can execute the chain.
        /// </summary>
        /// <returns>Start of the Chain.</returns>
        public abstract ChainStep<ProductT> Build();
    }
}
