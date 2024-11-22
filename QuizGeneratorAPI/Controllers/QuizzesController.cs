//using Microsoft.AspNetCore.Mvc;
//using QuizGeneratorAPI.Models;
//using QuizGeneratorAPI.Services;

//namespace QuizGeneratorAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class QuizzesController : ControllerBase
//    {
//        private readonly QuizService _quizService;

//        public QuizzesController(QuizService quizService)
//        {
//            _quizService = quizService;
//        }

//        // GET: api/quizzes
//        [HttpGet]
//        public IActionResult GetAllQuizzes()
//        {
//            var quizzes = _quizService.GetAllQuizzes();
//            return Ok(quizzes);
//        }

//        // GET: api/quizzes/5
//        [HttpGet("{id}")]
//        public IActionResult GetQuizById(int id)
//        {
//            var quiz = _quizService.GetQuizById(id);
//            if (quiz == null)
//            {
//                return NotFound("no data found");
//            }
//            return Ok(quiz);
//        }

//        // POST: api/quizzes
//        [HttpPost]
//        public IActionResult CreateQuiz([FromBody] Quiz quiz)
//        {
//            if (quiz == null)
//            {
//                return BadRequest();
//            }
//            var createdQuiz = _quizService.CreateQuiz(quiz);
//            return CreatedAtAction(nameof(GetQuizById), new { id = createdQuiz.Id }, createdQuiz);
//        }

//        // POST: api/quizzes/5/questions
//        [HttpPost("{quizId}/questions")]
//        public IActionResult AddQuestionToQuiz(int quizId, [FromBody] Question question)
//        {
//            if (question == null)
//            {
//                return BadRequest();
//            }
//            _quizService.AddQuestionToQuiz(quizId, question);
//            return Ok();
//        }


//        //remove data by id 


//    }
//}




//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using QuizGeneratorAPI.Data;
//using QuizGeneratorAPI.Models;
//using QuizGeneratorAPI.Services;

//namespace QuizGeneratorAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class QuizzesController : ControllerBase
//    {
//        private readonly QuizService _quizService;

//        private readonly QuizDbContext _context;

//        // Constructor with dependency injection
//        public QuizzesController(QuizDbContext context)
//        {
//            _context = context;
//        }
//        public QuizzesController(QuizService quizService)
//        {
//            _quizService = quizService;
//        }

//        // GET: api/quizzes
//        [HttpGet]
//        public IActionResult GetAllQuizzes()
//        {
//            var quizzes = _quizService.GetAllQuizzes();
//            return quizzes != null ? Ok(quizzes) : NotFound("No quizzes found.");
//        }

//        // GET: api/quizzes/5
//        [HttpGet("{id}")]
//        public IActionResult GetQuizById(int id)
//        {
//            var quiz = _quizService.GetQuizById(id);
//            if (quiz == null)
//            {
//                return NotFound($"Quiz with ID {id} not found.");
//            }
//            return Ok(quiz);
//        }

//        // POST: api/quizzes
//        [HttpPost]
//        public IActionResult CreateQuiz([FromBody] Quiz quiz)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (quiz.Questions != null && quiz.Questions.Any(q => q.QuizId != 0))
//            {
//                return BadRequest("Questions should not have a QuizId before being associated.");
//            }

//            var createdQuiz = _quizService.CreateQuiz(quiz);
//            return CreatedAtAction(nameof(GetQuizById), new { id = createdQuiz.Id }, createdQuiz);
//        }

//        // POST: api/quizzes/5/questions
//        [HttpPost("{quizId}/questions")]
//        public IActionResult AddQuestionToQuiz(int quizId, [FromBody] Question question)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (question.QuizId != 0 && question.QuizId != quizId)
//            {
//                return BadRequest("QuizId in the payload must match the route parameter.");
//            }

//            try
//            {
//                _quizService.AddQuestionToQuiz(quizId, question);
//                return Ok("Question added successfully.");
//            }
//            catch (KeyNotFoundException)
//            {
//                return NotFound($"Quiz with ID {quizId} not found.");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // DELETE: api/quizzes/5
//        [HttpDelete("{id}")]
//        public IActionResult DeleteQuizById(int id)
//        {
//            try
//            {
//                var deleted = _quizService.DeleteQuizById(id);
//                if (!deleted)
//                {
//                    return NotFound($"Quiz with ID {id} not found.");
//                }
//                return Ok($"Quiz with ID {id} deleted successfully.");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        [HttpPut("{id}")]
//        public IActionResult UpdateQuiz(int id, [FromBody] Quiz updatedQuiz)
//        {
//            if (updatedQuiz == null || id != updatedQuiz.Id)
//            {
//                return BadRequest("Invalid data.");
//            }

//            var existingQuiz = _context.Quizzes.FirstOrDefault(q => q.Id == id);

//            if (existingQuiz == null)
//            {
//                return NotFound("Quiz not found.");
//            }

//            // Update the fields of the existing entity with the new data
//            existingQuiz.Title = updatedQuiz.Title;

//            // If you need to update related entities (e.g., questions), you can do so here.
//            // Update other properties of the existingQuiz if needed.

//            _context.SaveChanges(); // Save the changes
//            return NoContent(); // Successfully updated
//        }




//    }
//}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizGeneratorAPI.Data;
using QuizGeneratorAPI.Models;
using QuizGeneratorAPI.Services;

namespace QuizGeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizService _quizService;
        private readonly QuizDbContext _context;

        // Combine both dependencies into one constructor
        public QuizzesController(QuizDbContext context, QuizService quizService)
        {
            _context = context;
            _quizService = quizService;
        }

        // GET: api/quizzes
        [HttpGet]
        public IActionResult GetAllQuizzes()
        {
            var quizzes = _quizService.GetAllQuizzes();
            return quizzes != null ? Ok(quizzes) : NotFound("No quizzes found.");
        }

        // GET: api/quizzes/5
        [HttpGet("{id}")]
        public IActionResult GetQuizById(int id)
        {
            var quiz = _quizService.GetQuizById(id);
            if (quiz == null)
            {
                return NotFound($"Quiz with ID {id} not found.");
            }
            return Ok(quiz);
        }

        // POST: api/quizzes
        [HttpPost]
        public IActionResult CreateQuiz([FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (quiz.Questions != null && quiz.Questions.Any(q => q.QuizId != 0))
            {
                return BadRequest("Questions should not have a QuizId before being associated.");
            }

            var createdQuiz = _quizService.CreateQuiz(quiz);
            return CreatedAtAction(nameof(GetQuizById), new { id = createdQuiz.Id }, createdQuiz);
        }

        // POST: api/quizzes/5/questions
        [HttpPost("{quizId}/questions")]
        public IActionResult AddQuestionToQuiz(int quizId, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (question.QuizId != 0 && question.QuizId != quizId)
            {
                return BadRequest("QuizId in the payload must match the route parameter.");
            }

            try
            {
                _quizService.AddQuestionToQuiz(quizId, question);
                return Ok("Question added successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Quiz with ID {quizId} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/quizzes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQuizById(int id)
        {
            try
            {
                var deleted = _quizService.DeleteQuizById(id);
                if (!deleted)
                {
                    return NotFound($"Quiz with ID {id} not found.");
                }
                return Ok($"Quiz with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/quizzes/5
        //[HttpPut("{id}")]
        //public IActionResult UpdateQuiz(int id, [FromBody] Quiz updatedQuiz)
        //{
        //    if (updatedQuiz == null || id != updatedQuiz.Id)
        //    {
        //        return BadRequest("Invalid data.");
        //    }

        //    var existingQuiz = _context.Quizzes.FirstOrDefault(q => q.Id == id);

        //    if (existingQuiz == null)
        //    {
        //        return NotFound("Quiz not found.");
        //    }

        //    // Update the fields of the existing entity with the new data
        //    existingQuiz.Title = updatedQuiz.Title;

        //    // If you need to update related entities (e.g., questions), you can do so here.
        //    // Update other properties of the existingQuiz if needed.

        //    _context.SaveChanges(); // Save the changes
        //    return NoContent(); // Successfully updated
        //}

        [HttpPut("{id}")]
        public IActionResult UpdateQuiz(int id, [FromBody] Quiz updatedQuiz)
        {
            if (updatedQuiz == null || id != updatedQuiz.Id)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                _quizService.UpdateQuiz(updatedQuiz); // Call the service to update the Quiz and Questions
                return NoContent(); // Return 204 No Content if the update was successful
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return 404 if Quiz is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 for any unexpected errors
            }
        }


    }
}
