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




using Microsoft.AspNetCore.Mvc;
using QuizGeneratorAPI.Models;
using QuizGeneratorAPI.Services;

namespace QuizGeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizzesController(QuizService quizService)
        {
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
    }
}
