using QuizApp.Models;

namespace QuizApp.Services.Interfaces
{
    public interface IQuestionService
    {
        int Save(Question question);
        int UpdateCorrectAnswerId(int questionId, int answerId);
    }
}
