using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
     
        public List<Answer>? Answers { get; set; } = new List<Answer>();

        [ForeignKey("CorrectAnswerId")]
        public int CorrectAnswerId { get; set; }
        public CorrectAnswer? CorrectAnswer { get; set; }


        public Question()
        {

            Answers = new List<Answer>
            {
                new Answer{Content = string.Empty, Question = this },
                new Answer{Content = string.Empty, Question = this },
                new Answer{Content = string.Empty, Question = this },
                new Answer{Content = string.Empty, Question = this }
            };

            
            CorrectAnswer = new CorrectAnswer();
        }
    }
}
