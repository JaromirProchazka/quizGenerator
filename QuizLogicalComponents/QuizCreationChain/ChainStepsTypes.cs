using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.QuizCreationChain
{
    /// <summary>
    /// Abstract Step of the Chain of responsibility, that results in the creation of the Topic with quiz from some source.
    /// </summary>
    public abstract class ITopicCreationStep
    {
        /// <summary>
        /// Next Step in the Chain
        /// </summary>
        public virtual ITopicCreationStep? Next { get; protected set; }

        /// <summary>
        /// Executes the user defined Step method and Starts the Next Step in the chain.
        /// </summary>
        public virtual void DoStep()
        {
            Step();

            if (Next != null) Next.DoStep();
        }

        /// <summary>
        /// The method characterizing the Step in the Chain. Override to implement your concreate Step.
        /// </summary>
        internal abstract void Step();
    }
}
