using HtmlAgilityPack;
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
    public abstract record class TopicCreationStep() : IDisposable
    {
        /// <summary>
        /// Next Step in the Chain.
        /// </summary>
        public virtual TopicCreationStep? Next { get; private set; }

        /// <summary>
        /// The Product of last Step. If none given, it is default initialized.
        /// </summary>
        public TopicProduct? BetweenStep = null;

        public virtual TopicCreationStep SetNext(TopicCreationStep next)
        {
            Next = next;
            return this;
        }

        public static string GetLabel() => "Step";

        /// <summary>
        /// Executes the user defined Step method and Starts the Next Step in the chain.
        /// </summary>
        public virtual TopicProduct DoStep()
        {
            var res = Step();

            if (Next != null)
            {
                Next.BetweenStep = res;
                return Next.DoStep();
            }
            else return res;
        }

        /// <summary>
        /// The method characterizing the Step in the Chain. Override to implement your concreate Step.
        /// </summary>
        internal abstract TopicProduct Step();

        /// <summary>
        /// Disposes Successors temporary data. (In override call base.Dispose();)
        /// </summary>
        public virtual void Dispose()
        {
            if (Next != null) Next?.Dispose();
        }
    }

    /// <summary>
    /// The product of topic creation.
    /// </summary>
    public record class TopicProduct()
    {
        /// <summary>
        /// A path to a local file where the final notes source is.
        /// </summary>
        public string? pathToSource { get; set; } = null;

        /// <summary>
        /// A path to a local file where the final quiz file is.
        /// </summary>
        public string? pathToQuiz { get; set; } = null;
    }
}
