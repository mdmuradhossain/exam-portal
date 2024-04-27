using exam_portal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace exam_portal.Controllers
{
    public class ExamController : Controller
    {
        private readonly DbContextSetup _context; // Replace YourDbContext with your actual DbContext class name
        //private readonly UserManager<IdentityUser> _userManager;
        public ExamController(DbContextSetup context)
        {
            _context = context;
           
        }

        // GET: Exam
        public IActionResult Index()
        {
            var exams = _context.Exams.ToList();
            ViewBag.Exams = exams;
            //return View(exams);
            return View();
        }

        // GET: Objects/View/5
        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }
           
            return View(exam);
        }

        // GET: Exam/Create
        public IActionResult Create()
        {

            var questions = _context.Questions.Include(q => q.Options).ToList();
            ViewBag.Questions = questions;

            return View();
        }

        // POST: Exam/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exam exam, IFormCollection form)
        {
            var selectedQuestionIds = form["SelectedQuestionIds"]
            .SelectMany(id => id.Split(','))
            .Select(id => int.Parse(id))
            .ToList();

            var selectedQuestions = _context.Questions
            .Where(q => selectedQuestionIds.Contains(q.QuestionId))
            .ToList();

            // Associate the selected questions with the exam
            exam.Questions = selectedQuestions;
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }

        // GET: Exam/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = _context.Exams.Find(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        // POST: Exam/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Exam exam)
        {
            if (id != exam.ExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.ExamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }

        // GET: Exam/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = _context.Exams
                .FirstOrDefault(m => m.ExamId == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (ExamExists(id))
            {
                var exam = _context.Exams.Find(id);
                _context.Exams.Remove(exam);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetQuestionsByUser()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user =  _userManager.GetUserAsync(User);
            //int userId = user.Id;
            //var userQuestions = _context.Questions.Where(q => q.UserId == userId).ToList();

            var questions = _context.Questions.Include(q => q.Options).ToList();
            ViewBag.Questions = questions;

            return View();
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.ExamId == id);
        }
    }
}
