namespace QuizApp.Models
{
    public class Test
    {
        public List<Question> questions { get; }
        public int currentQuestionId { get; set; }

        public int points { get; set; }

        private Question ShuffleAnswers(Question question)
        {
            HashSet<int> answerIdList = new HashSet<int>();
            List<Answer> newAnswer = new List<Answer>();

            Random random = new Random();

            while (answerIdList.Count != 4)
            {
                int tmpId = random.Next(0, 4);
                answerIdList.Add(tmpId);
            }

            foreach (int id in answerIdList)
            {
                newAnswer.Add(question.Answers[id]);
            }
            question.Answers = newAnswer;
            return question; 
        }

        public Test(List<Question> questions)
        {
            this.questions = questions;
            currentQuestionId = 0;
            points = 0;
        }
       
        public Question GetNextQuestion()
        {
            return ShuffleAnswers(questions[currentQuestionId]);
        }
            
    }
}
