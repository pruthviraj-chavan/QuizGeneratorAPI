using Microsoft.EntityFrameworkCore;
using QuizGeneratorAPI.Models;

namespace QuizGeneratorAPI.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
