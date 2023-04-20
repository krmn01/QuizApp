using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class CorrectAnswer
    {
        [Key, ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("Answer")]
        public int AnswerId { get; set; }

        public Question Question { get; set; }

        public Answer Answer { get; set; }
    }
}