using QuizApp.Models;

namespace QuizApp.Services.Interfaces
{
    public interface IAnswerService
    {
        int SaveAnswer(Answer answer);
        int SaveCorrectAnswer(Answer answer);
    }
}
