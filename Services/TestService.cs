using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Services.Interfaces;

namespace QuizApp.Services
{
    public class TestService : ITestService
    {

        private readonly AppDb _context;

        public TestService(AppDb context)
        {
            _context = context;
        }

        public List<Question> GenerateTest()
        {
            int questionCount = _context.questions.Count();
            HashSet<int> questionsId = new HashSet<int>();
            List<Question> newTest = new List<Question>();
            Random random = new Random();
            
            while (questionsId.Count != 10)
            {
                int tmpId = random.Next(1, questionCount);
                questionsId.Add(tmpId);
            }

            foreach(int id  in questionsId)
            {
                Question tmpQuestion = _context.questions.FirstOrDefault(x => x.Id == id);
                List<Answer> tmpAnswers = _context.answers
                    .Where(a => a.QuestionId == tmpQuestion.Id)
                    .ToList();
                tmpQuestion.Answers = tmpAnswers ?? new List<Answer>();
                newTest.Add(tmpQuestion);
            }
            return newTest;
        }
    }
}
