namespace QuizApp.Models
{
    public class Test
    {
        public List<Question> questions { get; }
        public int currentQuestionId { get; set; }

        public int points { get; set; }
        
        public Test(List<Question> questions)
        {
            this.questions = questions;
            currentQuestionId = 0;
            points = 0;
        }
       
        public Question GetNextQuestion()
        {
            return questions[currentQuestionId];
        }
            
    }
}
