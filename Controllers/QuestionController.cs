using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Services.Interfaces;
using QuizApp.Services;
using System.Net.Http.Headers;

namespace QuizApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;

        public QuestionController(IQuestionService questionService, IAnswerService answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            //return View(new Question());
            return View();
        }

        [HttpPost]
        public IActionResult AddQuestion(Question body)
        {
            if (!ModelState.IsValid)
            {
                return View(body);
            }

            if (body.CorrectAnswerId >= body.Answers.Count)
            {
                ModelState.AddModelError("CorrectAnswerId", "Invalid index for correct answer");
                return View(body);
            }
/*
            var correctAnswer = new CorrectAnswer
            {
                Answer = body.Answers[body.CorrectAnswerId],
                Question = body,
                AnswerId = body.Answers[body.CorrectAnswerId].Id,
                QuestionId = body.Id
        };*/

            var questionID = _questionService.Save(body);

            /*foreach (var answer in body.Answers)
            {
                answer.Question = body;
                _answerService.SaveAnswer(answer);
            }

            
            */
            var corrID = _answerService.SaveCorrectAnswer(body.Answers[body.CorrectAnswerId]);
            
            _questionService.UpdateCorrectAnswerId(questionID, corrID);
            
            

            return RedirectToAction("AddQuestion");
        }
    }
}
