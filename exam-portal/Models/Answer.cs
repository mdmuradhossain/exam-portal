using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace exam_portal.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public int? AnswerExamId { get; set; } = 0;
        public int? AnswerQuestionId { get; set; } = 0;
        public int? SelectedOptionId { get; set; }
        public string TextAnswer { get; set; }

        // Navigation properties
        //public virtual Exam? Exam { get; set; }
       //public virtual Question? Question { get; set; }
    }

}
