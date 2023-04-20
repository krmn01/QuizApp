using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }  
        public List<Answer> Answers { get; set; } = new List<Answer>();

        public int CorrectAnswerId { get; set; }

        [ForeignKey("CorrectAnswerId")]
        public CorrectAnswer CorrectAnswer { get; set; }
    }
}
