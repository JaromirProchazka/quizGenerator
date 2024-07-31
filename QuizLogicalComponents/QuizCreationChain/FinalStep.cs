using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogicalComponents.QuizCreationChain
{
    /// <summary>
    /// an @ITopicCreationStep, which simply returns the result ending the Chain. 
    /// </summary>
    public sealed class Finalize : ITopicCreationStep
    {
        public override ITopicCreationStep? Next { get => null; }

        public override void DoStep()
        {
            Step();
        }
    }
}
