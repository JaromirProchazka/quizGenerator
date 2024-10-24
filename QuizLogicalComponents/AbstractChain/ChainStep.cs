using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.AbstractChain
{
    /// <summary>
    /// A Step abstract type for a chain of responcibility.
    /// </summary>
    /// <typeparam name="ProductT">The type of the Product of the chain. Should implement Default constructor.</typeparam>
    public abstract record class ChainStep<ProductT>
        where ProductT : ChainProduct
    {
        /// <summary>
        /// Next Step in the Chain.
        /// </summary>
        public virtual ChainStep<ProductT>? Next { get; protected set; }

        /// <summary>
        /// A label identifying the step.
        /// </summary>
        /// <returns>The label</returns>
        public static string GetLabel() => "Step";

        /// <summary>
        /// The Product of last Step. If none given, it is default initialized.
        /// </summary>
        public virtual ProductT? BetweenStep { get; set; }

        /// <summary>
        /// The Final result of the chain. If null, the result isn't computed yet.
        /// </summary>
        public ProductT? Result { get; protected set; } = null;

        /// <summary>
        /// Adds a step to the chain.
        /// </summary>
        /// <param name="next">The next step to add</param>
        /// <returns>return the Step, to which the next one was added</returns>
        public virtual ChainStep<ProductT> SetNext(ChainStep<ProductT> next)
        {
            Next = next;
            return this;
        }

        /// <summary>
        /// Executes the user defined Step method and Starts the Next Step in the chain.
        /// </summary>
        public virtual ProductT DoStep()
        {
            var betweenResult = Step();

            if (Next != null)
            {
                Next.BetweenStep = betweenResult;
                Result = Next.DoStep();
                // Here, the whole chain was transited
                return Result;
            }
            else return betweenResult; // Here, this is the final result
        }

        /// <summary>
        /// The method characterizing the Step in the Chain. Override to implement your concreate Step.
        /// </summary>
        public abstract ProductT Step();
    }

    public record class ChainProduct()
    {

    }
}
