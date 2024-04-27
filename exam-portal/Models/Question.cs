using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_portal.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        //public int ExamId { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<OptionModel> Options { get; set; }

        //public int UserId { get; set; }
        // Foreign key property
        public int? ExamId { get; set; } = 0;

        // Navigation property
        public Exam? Exam { get; set; }

    }

    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
        Essay
    }
}
