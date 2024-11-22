//using Microsoft.EntityFrameworkCore;
//using QuizGeneratorAPI.Models;
//using QuizGeneratorAPI.Data;

//namespace QuizGeneratorAPI.Services
//{
//    public class QuizService
//    {
//        private readonly QuizDbContext _context;

//        public QuizService(QuizDbContext context)
//        {
//            _context = context;
//        }

//        public List<Quiz> GetAllQuizzes()
//        {
//            return _context.Quizzes.Include(q => q.Questions).ToList();
//        }

//        public Quiz GetQuizById(int id)
//        {
//            return _context.Quizzes.Include(q => q.Questions)
//                                   .FirstOrDefault(q => q.Id == id);
//        }

//        public Quiz CreateQuiz(Quiz quiz)
//        {
//            _context.Quizzes.Add(quiz);
//            _context.SaveChanges();
//            return quiz;
//        }

//        public void AddQuestionToQuiz(int quizId, Question question)
//        {
//            var quiz = _context.Quizzes.Include(q => q.Questions)
//                                        .FirstOrDefault(q => q.Id == quizId);

//            if (quiz != null)
//            {
//                question.QuizId = quizId;
//                _context.Questions.Add(question);
//                _context.SaveChanges();
//            }
//        }
//    }
//}




using Microsoft.EntityFrameworkCore;
using QuizGeneratorAPI.Models;
using QuizGeneratorAPI.Data;

namespace QuizGeneratorAPI.Services
{
    public class QuizService
    {
        private readonly QuizDbContext _context;

        public QuizService(QuizDbContext context)
        {
            _context = context;
        }

        public List<Quiz> GetAllQuizzes()
        {
            return _context.Quizzes.Include(q => q.Questions).ToList();
        }

        public Quiz GetQuizById(int id)
        {
            return _context.Quizzes.Include(q => q.Questions)
                                   .FirstOrDefault(q => q.Id == id);
        }

        public Quiz CreateQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return quiz;
        }

        public void AddQuestionToQuiz(int quizId, Question question)
        {
            var quiz = _context.Quizzes.Include(q => q.Questions)
                                        .FirstOrDefault(q => q.Id == quizId);

            if (quiz != null)
            {
                question.QuizId = quizId;
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
        }

        public bool DeleteQuizById(int id)
        {
            var quiz = _context.Quizzes.Include(q => q.Questions)
                                       .FirstOrDefault(q => q.Id == id);

            if (quiz == null)
            {
                return false; // Quiz not found
            }

            _context.Questions.RemoveRange(quiz.Questions); // Remove associated questions
            _context.Quizzes.Remove(quiz); // Remove the quiz itself
            _context.SaveChanges();
            return true;
        }
    }
}
