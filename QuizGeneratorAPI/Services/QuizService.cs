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

        //public void UpdateQuiz(Quiz quiz)
        //{
        //    _context.Quizzes.Update(quiz);
        //    _context.SaveChanges();
        //}


        //public void UpdateQuiz(Quiz updatedQuiz)
        //{
        //    // Check if the updated quiz exists in the database
        //    var existingQuiz = _context.Quizzes.Include(q => q.Questions)
        //                                       .FirstOrDefault(q => q.Id == updatedQuiz.Id);

        //    if (existingQuiz == null)
        //    {
        //        throw new InvalidOperationException("Quiz not found.");
        //    }

        //    // Update Quiz properties
        //    existingQuiz.Title = updatedQuiz.Title;

        //    // Handle the associated questions
        //    foreach (var updatedQuestion in updatedQuiz.Questions)
        //    {
        //        var existingQuestion = existingQuiz.Questions.FirstOrDefault(q => q.Id == updatedQuestion.Id);

        //        if (existingQuestion != null)
        //        {
        //            // Update the existing question
        //            existingQuestion.Text = updatedQuestion.Text;
        //            existingQuestion.Options = updatedQuestion.Options ?? new List<string>(); // Ensure options are not null
        //            existingQuestion.CorrectOptionIndex = updatedQuestion.CorrectOptionIndex;
        //        }
        //        else
        //        {
        //            // If the question is new, add it
        //            updatedQuestion.QuizId = existingQuiz.Id; // Ensure the quiz association is set
        //            _context.Questions.Add(updatedQuestion);
        //        }
        //    }

        //    // Remove questions that are no longer part of the updatedQuiz
        //    var existingQuestionIds = existingQuiz.Questions.Select(q => q.Id).ToList();
        //    var updatedQuestionIds = updatedQuiz.Questions.Select(q => q.Id).ToList();

        //    var questionsToRemove = existingQuiz.Questions.Where(q => !updatedQuestionIds.Contains(q.Id)).ToList();
        //    _context.Questions.RemoveRange(questionsToRemove);

        //    // Save changes to the database
        //    _context.SaveChanges();
        //}


        public void UpdateQuiz(Quiz updatedQuiz)
        {
            var existingQuiz = _context.Quizzes.Include(q => q.Questions)
                                               .FirstOrDefault(q => q.Id == updatedQuiz.Id);

            if (existingQuiz == null)
            {
                throw new InvalidOperationException("Quiz not found.");
            }

            // Update Quiz properties
            existingQuiz.Title = updatedQuiz.Title;

            // Loop through the updated questions
            foreach (var updatedQuestion in updatedQuiz.Questions)
            {
                var existingQuestion = existingQuiz.Questions.FirstOrDefault(q => q.Id == updatedQuestion.Id);

                if (existingQuestion != null)
                {
                    // Update existing question properties
                    existingQuestion.Text = updatedQuestion.Text;
                    existingQuestion.Options = updatedQuestion.Options ?? new List<string>();
                    existingQuestion.CorrectOptionIndex = updatedQuestion.CorrectOptionIndex;
                }
                else
                {
                    // Add new question if it doesn't exist
                    updatedQuestion.QuizId = existingQuiz.Id; // Set the foreign key to the current QuizId
                    _context.Questions.Add(updatedQuestion);
                }
            }

            // Handle deletion of questions that are no longer part of the quiz
            var existingQuestionIds = existingQuiz.Questions.Select(q => q.Id).ToList();
            var updatedQuestionIds = updatedQuiz.Questions.Select(q => q.Id).ToList();

            var questionsToRemove = existingQuiz.Questions.Where(q => !updatedQuestionIds.Contains(q.Id)).ToList();
            _context.Questions.RemoveRange(questionsToRemove);

            // Save all changes to the database
            _context.SaveChanges();
        }



    }
}
