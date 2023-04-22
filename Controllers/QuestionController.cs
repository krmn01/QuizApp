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

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View("AddQuestion", new Question());
        }

        [HttpPost]
        public IActionResult AddQuestion(Question body)
        {

            
            /*
            foreach (var answer in body.Answers)
            {
                answer.Question = body;
            }

            body.CorrectAnswer = new CorrectAnswer
            {
                Answer = body.Answers[body.CorrectAnswerId],
                AnswerId = body.Answers[body.CorrectAnswerId].Id,
                Question = body
            };*/

            if (!ModelState.IsValid)
            {
                return View(body);
            }

            _questionService.Save(body);
            

            return Redirect("AddQuestion");
        }
    }
}
