using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Services.Interfaces;

namespace QuizApp.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly AppDb _context;

        public AnswerService(AppDb context)
        {
            _context = context;
        }
        public int SaveAnswer(Answer answer)
        {
            var newAnswer = new Answer
            {
                Content = answer.Content,
                QuestionId = answer.QuestionId
            };
            
            _context.answers.Add(newAnswer);
            _context.SaveChanges();

            return answer.Id;
        }

        public int SaveCorrectAnswer(Answer answer)
        {
            var newAnswer = new CorrectAnswer
            {
                AnswerId = answer.Id,
                QuestionId = answer.QuestionId
            };

            _context.correctAnswers.Add(newAnswer);
            _context.SaveChanges();

            return 1;
        }
    }
}
