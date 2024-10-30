using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizPersistence;

namespace QuizPersistence.QuizStates
{
    /// <summary>
    /// General interface for Quiz State serializers
    /// </summary>
    /// <typeparam name="QT">Quiz state type implementing default constructor</typeparam>
    public interface IStateSerializer<QT> where QT : QuizState
    {
        /// <summary>
        /// Loads state from Quiz data file
        /// </summary>
        /// <returns>Quiz State instance either from memory, or newly initialized one using the @IQuizState.NewState method</returns>
        public QuizState? LoadState();

        /// <summary>
        /// Saves the Quiz state to the Topics data folder.
        /// </summary>
        /// <param name="state"> The Quiz state to save</param>
        /// <returns>Status of the save (successful)</returns>
        public bool StoreState(QT state);
    }

    public record class BaseStateSerializer<QST> : 
        IStateSerializer<QST> where QST : QuizState, new()
    {
        /// <summary>
        /// Path to the questions file in topics folder
        /// </summary>
        string QuestionsFilePath;
        public string Name;

        /// <summary>
        /// A basic State serializer serializing to JSON using <see cref="JsonSerializer"/>
        /// </summary>
        /// <param name="path">Path to the questions file in topics folder</param>
        public BaseStateSerializer(string path)
        {
            QuestionsFilePath = path;
            if (!File.Exists(path)) throw new Exception("Given path to Questions isn't valid!");
            Name = QuestionsFile.GetQuizName(QuestionsFilePath);
        }


        public QuizState LoadState()
        {
            var loadedData = QuestionsFile.LoadQuizStateData(Name);
            if (loadedData.State == QuestionsFile.LoadedState.NotFound) 
                return new QST().NewState(QuestionsFilePath);

            var deser = JsonSerializer.Deserialize<QST>(loadedData.Data);
            if (deser == null)
                return new QST().NewState(QuestionsFilePath);

            return deser
                .InitUntrackedFields();
        }

        public bool StoreState(QST state)
        {
            string stateData = JsonSerializer.Serialize(state);
            return QuestionsFile.SaveQuizState(Name, stateData);
        }
    }
}
