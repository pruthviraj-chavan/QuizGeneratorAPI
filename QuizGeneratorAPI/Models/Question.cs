using QuizGeneratorAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Question
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; }

    public List<string> Options { get; set; }

    public int CorrectOptionIndex { get; set; }

    [ForeignKey("Quiz")]
    public int QuizId { get; set; }

    // Mark Quiz as optional for model binding purposes
    public Quiz? Quiz { get; set; }
}
