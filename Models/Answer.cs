using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; } 
        public string Content { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question question { get; set; }

    }
}
