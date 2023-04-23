using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Services.Interfaces;

namespace QuizApp.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDb _context;

        public QuestionService(AppDb context)
        {
            _context = context;
        }

        public int Save(Question question)
        {
            var newQuestion = new Question
            {
                Content = question.Content,
                Answers = question.Answers,
                CorrectAnswer = question.CorrectAnswer,
                CorrectAnswerId = question.CorrectAnswerId
            };

            _context.questions.Add(newQuestion);
            _context.SaveChanges();



            return newQuestion.Id;
        }

        public int UpdateCorrectAnswerId(int questionId, int answerId)
        {
            var question = _context.questions.Find(questionId);

            if (question != null)
            {
                question.CorrectAnswerId = answerId;

                _context.Entry(question).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return 1;
        }
    }
}
