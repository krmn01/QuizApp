using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Services.Interfaces;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace QuizApp.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService) 
        {
            _testService = testService;
        }

        [HttpGet]
        public IActionResult StartTest()
        {
            
            Test test = new Test(_testService.GenerateTest());
            byte[] testBytes = JsonSerializer.SerializeToUtf8Bytes(test);

            HttpContext.Session.Set("Test",testBytes);

            return RedirectToAction("GetTest", new { questionId = test.currentQuestionId });
        }

        [HttpPost]
        public IActionResult GetNextQuestion(int answerId)
        {
            byte[] testBytes = HttpContext.Session.Get("Test");
            Test test = JsonSerializer.Deserialize<Test>(testBytes);

            // Update current question ID
            test.currentQuestionId++;

            // Check if all questions have been answered
            if (test.currentQuestionId >= test.questions.Count)
            {
                // TODO: Handle end of test
                return View("TestEnd");
            }

            // Update the selected answer for the current question
            Question currentQuestion = test.questions[test.currentQuestionId - 1];
            Answer selectedAnswer = currentQuestion.Answers.FirstOrDefault(a => a.Id == answerId);
            if (selectedAnswer != null)
            {
               
            }

            // Get the next question
            Question nextQuestion = test.questions[test.currentQuestionId];

            // Store the updated test object in session
            testBytes = JsonSerializer.SerializeToUtf8Bytes(test);
            HttpContext.Session.Set("Test", testBytes);

            // Return the next question as JSON
            return Json(nextQuestion);
        }
    

    [HttpGet]
    public JsonResult GetNextQuestion()
    {
            // Get the current test from the session
            byte[] testBytes = HttpContext.Session.Get("Test");
            Test test = JsonSerializer.Deserialize<Test>(testBytes);


            // Get the next question
            var nextQuestion = test.GetNextQuestion();

        // Return the next question as JSON
        return Json(nextQuestion);
    }

    [HttpGet]
        public IActionResult GetTest(Question quest)
        {
            byte[] testBytes = HttpContext.Session.Get("Test");
            Test? test = JsonSerializer.Deserialize<Test>(testBytes);

            if (test == null)
            {
                return Redirect("StartTest");
            }

            Question question = test.questions[test.currentQuestionId];

            ViewData["Test"] = test;
            ViewData["Question"] = question;
            TempData["CurrenQuestionId"] = test.currentQuestionId;
            ViewData["QuestionsCount"] = test.questions.Count;
            return View();
        }
    }
}
