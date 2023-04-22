using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class CorrectAnswer
    {
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }

        [Key,ForeignKey("AnswerId")]
        public int AnswerId { get; set; }

       
        public Question? Question { get; set; }

        public Answer? Answer { get; set; }
    }
}