using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLogicalComponents;
using QuizLogicalComponents.QuizStates;

namespace QuizLogicalComponentsTests
{
    [TestClass()]
    public class BaseStateSerializerTests
    {
        private string testPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".sources", "TestTopic", "questions.html");

        //// TODO: Finish tests
        //[TestMethod()]
        //public void LoadState_CorrectState()
        //{
        //    var serializer = new BaseStateSerializer<ResetAroundState>(testPath);
        //    var state = new ResetAroundState(testPath);
        //}
    }
}
