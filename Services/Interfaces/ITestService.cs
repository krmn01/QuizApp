using QuizApp.Models;

namespace QuizApp.Services.Interfaces
{
    public interface ITestService
    {
        public List<Question> GenerateTest();
        public bool CheckAnswer(int questionId, int answerId);
    }
}
