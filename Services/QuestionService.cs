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
            var correctAnswer = question.Answers?.Single(a => a == question.Answers[question.CorrectAnswerId]);
            question.CorrectAnswer = new CorrectAnswer { Answer = correctAnswer, AnswerId = correctAnswer.Id, QuestionId = question.Id};
            _context.correctAnswers.Add(question.CorrectAnswer);
            _context.SaveChanges();

            var newQuestion = new Question
            {
                Content = question.Content,
                Answers = question.Answers.Select(a => new Answer
                {
                    Content = a.Content,
                    Question = question
                }).ToList(),
                CorrectAnswerId = question.CorrectAnswerId
            };

            _context.questions.Add(newQuestion);
            _context.SaveChanges();

            foreach (var a in newQuestion.Answers)
            {
                _context.answers.Add(a);
            }

            _context.SaveChanges();

            return newQuestion.Id;
        }
    }
}
