using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
     
        public List<Answer>? Answers { get; set; } = new List<Answer>();

        [Required]
        public int CorrectAnswerId { get; set; }
        public CorrectAnswer? CorrectAnswer { get; set; }


      
    }
}
