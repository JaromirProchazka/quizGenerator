using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesParsing;
using Refit;

namespace TopicCreation.TopicCreationChain
{
    /// <summary>
    /// Determines the format of the html nodes, which will be taken as Questions. 
    /// </summary>
    public sealed record class ChooseQuestionFormat : TopicCreationStep
    {
        /// <summary>
        /// The Analyzer of nodes, which state determines the format.
        /// </summary>
        public QuestionNodeParams Analyser;
        /// <summary>
        /// Denotes, if the given sought after attributes are required all or at least one for it to be determined as question node.
        /// </summary>
        public enum ChecksLogicalOperator { AND, OR };

        /// <summary>
        /// Default Question format setting.
        /// </summary>
        public ChooseQuestionFormat()
        {
            Analyser = NotesParser.DefaultAnalyzer;
        }

        /// <summary>
        /// Set Question format parameters.
        /// </summary>
        /// <param name="logicalOperator">Determines, if the given sought after attributes are required all or at least one for it to be determined as question node.</param>
        /// <param name="name">The Nodes name (in the <>)</param>
        /// <param name="classes">Nodes classes separated with ',', if any is present, this check passes.</param>
        /// <param name="color">Nodes text color</param>
        /// <param name="fontFamily">any font-family name</param>
        public ChooseQuestionFormat(ChecksLogicalOperator logicalOperator, string? name, string? classes, string? color, string? fontFamily)
        {
            switch (logicalOperator)
            {
                case ChecksLogicalOperator.AND:
                    Analyser = new AndQuestionNodeParams();
                    break;
                case ChecksLogicalOperator.OR:
                    Analyser = new OrQuestionNodeParams();
                    break;
                default:
                    Analyser = new AndQuestionNodeParams();
                    break;
            }

            Analyser
                .SetName(name)
                .SetClasses(classes)
                .SetColor(color)
                .SetFontFamily(fontFamily);
        }

        public override TopicProduct Step()
        {
            QuizPersistence.QuestionsFile.SetQuestionNodeAnalyzer(Analyser);
            if (BetweenStep == null) BetweenStep = new TopicProduct();
            return BetweenStep;
        }

        public override void Dispose()
        {
            base.Dispose();

            QuizPersistence.QuestionsFile.SetQuestionNodeAnalyzer(NotesParser.DefaultAnalyzer);
        }
    }
}
