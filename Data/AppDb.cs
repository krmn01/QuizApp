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
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CorrectAnswer>()
                .HasOne(ca => ca.Question)
                .WithOne(q => q.CorrectAnswer)
                .HasForeignKey<Question>(q => q.CorrectAnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CorrectAnswer>()
                .HasOne(ca => ca.Answer)
                .WithMany()
                .HasForeignKey(ca => ca.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.CorrectAnswer)
                .WithOne(ca => ca.Question)
                .HasForeignKey<Question>(q => q.CorrectAnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> questions { get; set; }
        public DbSet<Answer> answers { get; set; }

        public DbSet<CorrectAnswer> correctAnswers { get; set; }
    }
}
