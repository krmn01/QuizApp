using QuizApp.Models;

namespace QuizApp.Services.Interfaces
{
    public interface ITestService
    {
        public List<Question> GenerateTest();
    }
}
