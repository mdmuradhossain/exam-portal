using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace exam_portal.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public string CourseTitle { get; set; }

        public string? DepartmentName { get; set; }
        public string? Description { get; set; }
        public int TotalMarks { get; set; }
        public TimeSpan Duration { get; set; }

        //public int QuestionId { get; set; }

        //public List<Question> Questions { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
