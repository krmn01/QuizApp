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


            if (_testService.CheckAnswer(test.questions[test.currentQuestionId].Id, answerId))
            {
                test.points += 1;
            }


            test.currentQuestionId++;
  
            if (test.currentQuestionId >= test.questions.Count)
            {
                TempData["Points"] = test.points;
                TempData["Size"] = test.questions.Count;
                return Json(new { redirectUrl = Url.Action("TestEnd") });
            }

            Question nextQuestion = test.questions[test.currentQuestionId];

            // Store the updated test object in session
            testBytes = JsonSerializer.SerializeToUtf8Bytes(test);
            HttpContext.Session.Set("Test", testBytes);

            return Json(new { id = nextQuestion.Id, content = nextQuestion.Content, answers = nextQuestion.Answers, currentQuestionId = test.currentQuestionId });
        }

     [HttpGet]
     public IActionResult TestEnd()
       {
            byte[] testBytes = HttpContext.Session.Get("Test");
            if (testBytes == null) return RedirectToAction("StartTest");
            
            Test? test = JsonSerializer.Deserialize<Test>(testBytes);
            return View();
       }

    [HttpGet]
    public JsonResult GetNextQuestion()
    {
            byte[] testBytes = HttpContext.Session.Get("Test");
            Test? test = JsonSerializer.Deserialize<Test>(testBytes);

            Console.WriteLine($"currentQuestionId: {test.currentQuestionId}, questions count: {test.questions.Count}");
            
            var nextQuestion = test.GetNextQuestion();

        return Json(new { id = nextQuestion.Id, content = nextQuestion.Content, answers = nextQuestion.Answers, currentQuestionId = test.currentQuestionId });
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
            TempData["CurrentQuestionId"] = test.currentQuestionId;
            ViewData["QuestionsCount"] = test.questions.Count;
            return View();
        }
    }
}
