using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data
{
    public class AppDb:DbContext
    {
        public AppDb(DbContextOptions<AppDb>options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            modelBuilder.Entity<CorrectAnswer>()
                .HasOne(ca => ca.Question)
                .WithOne(q => q.CorrectAnswer)
                .HasForeignKey<Question>(q => q.CorrectAnswerId);

            modelBuilder.Entity<CorrectAnswer>()
                .HasOne(ca => ca.Answer)
                .WithMany()
                .HasForeignKey(ca => ca.AnswerId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> questions { get; set; }
        public DbSet<Answer> answers { get; set; }

        public DbSet<CorrectAnswer> correctAnswers { get; set; }
    }
}
