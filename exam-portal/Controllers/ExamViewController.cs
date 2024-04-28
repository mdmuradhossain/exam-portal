using exam_portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace exam_portal.Controllers
{
    public class ExamViewController : Controller
    {
        private readonly DbContextSetup _context;

        public ExamViewController(DbContextSetup context)
        {
            _context = context;

        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var exam =  _context.Exams.Find(id);
            var exam = _context.Exams.Include(e => e.Questions) // Include the associated questions
            .ThenInclude(q => q.Options) // Include the options for each question
        .FirstOrDefault(e => e.ExamId == id);

            if (exam == null)
            {
                return NotFound();
            }

            var viewModel = new ExamAnswerViewModel
            {
                Exam = exam
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(ExamAnswerViewModel viewModel)
        {
            if (viewModel.Answers == null || viewModel.Exam.Questions == null)
            {
                // Handle case where no answers are submitted or no questions exist
                // Redirect to exam index or any appropriate action
                return RedirectToAction(nameof(Answers)); // Redirect to exam index or any appropriate action
            }

            // Process submitted answers and save to the database
            for (int i = 0; i < viewModel.Answers.Count; i++)
            {
                var answer = viewModel.Answers[i];
                var questionId = viewModel.Exam.Questions.ElementAtOrDefault(i)?.QuestionId;

                if (questionId != null)
                {
                    // Create a new Answer object for each submitted answer
                    var newAnswer = new Answer
                    {
                        AnswerExamId = viewModel.Exam.ExamId,
                        AnswerQuestionId = questionId.Value,
                        TextAnswer = answer.TextAnswer
                    };

                    // Add the answer to the database context
                    _context.Answers.Add(newAnswer);
                }
            }

            // Save changes to the database
            _context.SaveChanges();

            // Optionally, you can redirect to a confirmation page or display a message
            return RedirectToAction(nameof(Answers)); // Redirect to a confirmation action or any appropriate action
        }



        public IActionResult Answers()
        {
            return View();
        }

    }
}
