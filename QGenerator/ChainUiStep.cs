using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLogicalComponents;
using QuizLogicalComponents.AbstractChain;

namespace QGenerator
{
    public class ChainStepForm<StepT, ProductT, BuilderT> : Form
        where ProductT : ChainProduct, new()
        where StepT : ChainStep<ProductT>
        where BuilderT : ChainBuilder<StepT, ProductT>, new()
    {
        /// <summary>
        /// The next step window to be loaded. If null, this is the last step.
        /// </summary>
        internal ChainStepForm<StepT, ProductT, BuilderT>? Next = null;
        /// <summary>
        /// Setter for the <see cref="Next"/>, and returns its new value for the fluent syntax.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public ChainStepForm<StepT, ProductT, BuilderT> SNext(ChainStepForm<StepT, ProductT, BuilderT> next)
        {
            Next = next;
            return Next;
        }
        /// <summary>
        /// The running builder of the Chain.
        /// </summary>
        internal BuilderT Builder;
        public ChainStepForm<StepT, ProductT, BuilderT> SBuilder(BuilderT builder)
        {
            Builder = builder;
            return this;
        }

        public ChainStepForm(BuilderT bulder) { 
            Builder = bulder;
        }

        /// <summary>
        /// Does all the jobs before this window is ready to close.
        /// </summary>
        public ProductT? Finalize()
        {
            if (Next == null)
            {
                return Builder.Build().DoStep();
            }
            else
            {
                Next.Builder = Builder;
                Next.Show();
            }

            return null;
        }

    }
}
