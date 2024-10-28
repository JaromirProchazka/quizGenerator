using HtmlAgilityPack;
using QuizLogicalComponents.AbstractChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicCreation.TopicCreationChain
{
    /// <summary>
    /// Abstract Step of the Chain of responsibility, that results in the creation of the Topic with quiz from some source.
    /// </summary>
    public abstract record class TopicCreationStep :
        ChainStep<TopicProduct>, IDisposable
    {
        //public override ChainStep<TopicProduct>? Next { get; protected set; }

        /// <summary>
        /// Disposes Successors temporary data. (In override call base.Dispose();)
        /// </summary>
        public virtual void Dispose()
        {
            if (Next != null) ((TopicCreationStep)Next)?.Dispose();
        }
    }

    /// <summary>
    /// The product of topic creation.
    /// </summary>
    public record class TopicProduct() : ChainProduct()
    {
        /// <summary>
        /// A path to a local file where the final notes source is.
        /// </summary>
        public string? pathToSource { get; set; } = null;

        /// <summary>
        /// A path to a local file where the final quiz file is.
        /// </summary>
        public string? pathToQuiz { get; set; } = null;

        /// <summary>
        /// The final action to do on the Topic being created, like updating the list of topics.
        /// </summary>
        public Action? finalize { get; set; } = null;
    }
}
