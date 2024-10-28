using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using QuizGeneratorPresentation.QuizStarting;
using QuizGeneratorPresentation.TopicCreation;

using TopicCreation.TopicCreationChain;
using QuizStarting.TopicStartingChain;

namespace QuizGeneratorPresentation
{
    public static class ChainUiInit
    {
        /// <summary>
        /// Getter for a Chain of responsibility, which is responsible for Creating new Topic.
        /// </summary>
        /// <param name="finalize">A function to be called on the topic creation, for instance a Update of list of topics.</param>
        /// <returns>The First Step Form ready to be Shown.</returns>
        public static ChainStepForm<TopicCreationStep,TopicProduct,ChainCreationBuilder> GetCreationChain(Action finalize)
        {
            var builder = new ChainCreationBuilder();
            var chain = new ChooseSourceStep(finalize).SBuilder(builder); // first step
            _ = chain.SNext(new ChooseQuestionsFormat());

            return chain;
        }

        /// <summary>
        /// Getter for a Chain of responsibility, which is responsible for Starting the Quiz.
        /// </summary>
        /// <param name="pathToQuiz">A file path to the quiz folder.</param>
        /// <returns>The First Step Form ready to be Shown.</returns>
        public static ChainStepForm<QuizStartingStep,QuizProduct,ChainStartingBuilder> GetStartingChain(string pathToQuiz)
        {
            var builder = new ChainStartingBuilder();
            var chain = new ChooseQuizBeginningFrom(pathToQuiz).SBuilder(builder);
            //_ = chain.SNext(  );

            return chain;
        }
    }
}
