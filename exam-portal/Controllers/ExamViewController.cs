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
            if (!ModelState.IsValid || viewModel.Answers == null || viewModel.Exam.Questions == null)
            {
                // Handle invalid ModelState or missing answers/questions
                // Redirect to exam index or any appropriate action
                return RedirectToAction(nameof(Answers));
            }

            // Process and save submitted answers to the database
            foreach (var answer in viewModel.Answers)
            {
                if (answer.AnswerId != null)
                {
                    // Create a new Answer object for each submitted answer
                    var newAnswer = new Answer
                    {
                        AnswerExamId = viewModel.Exam.ExamId,
                        AnswerQuestionId = answer.AnswerId,
                        TextAnswer = answer.TextAnswer
                    };

                    // Add the answer to the database context
                    _context.Answers.Add(newAnswer);
                }
            }

            // Save changes to the database
            _context.SaveChanges();

            // Optionally, you can redirect to a confirmation page or display a message
            return RedirectToAction(nameof(Answers));
        }


        public IActionResult Answers()
        {
            return View();
        }

    }
}
