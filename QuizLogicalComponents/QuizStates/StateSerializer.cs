using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FileManager;

namespace QuizLogicalComponents.QuizStates
{
    /// <summary>
    /// General interface for Quiz State serializers
    /// </summary>
    /// <typeparam name="QT">Quiz state type implementing default constructor</typeparam>
    public interface IStateSerializer<QT> where QT : IQuizState, new()
    {
        /// <summary>
        /// Loads state from Quiz data file
        /// </summary>
        /// <returns>Quiz State instance either from memory, or newly initialized one using the @IQuizState.NewState method</returns>
        public IQuizState LoadState();

        /// <summary>
        /// Saves the Quiz state to the Topics data folder.
        /// </summary>
        /// <param name="state"> The Quiz state to save</param>
        /// <returns>Status of the save (successful)</returns>
        public bool StoreState(QT state);
    }

    /// <summary>
    /// A basic State serializer serializing to JSON using @JsonSerializer
    /// </summary>
    /// <typeparam name="QST">State type implementing default constructor</typeparam>
    /// <param name="Path">Path to the questions file in topics folder</param>
    public record class BaseStateSerializer<QST>(string Path) : 
        IStateSerializer<QST> where QST : IQuizState, new()
    {
        public string Name = QuestionsFile.GetQuizName(Path);

        public IQuizState LoadState()
        {
            var loadedData = QuestionsFile.LoadQuizStateData(Name);
            if (loadedData.State == QuestionsFile.LoadedState.NotFound) 
                return new QST().NewState(Path);
            return JsonSerializer.Deserialize<QST>(loadedData.Data)
                .InitUntrackedFields();
        }

        public bool StoreState(QST state)
        {
            string stateData = JsonSerializer.Serialize(state);
            return QuestionsFile.SaveQuizState(Name, stateData);
        }
    }
}
