using exam_portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Web;
using System.Security;
using Microsoft.AspNetCore.Identity;

namespace exam_portal.Controllers
{
    public class QuestionController : Controller
    {
        private readonly DbContextSetup _context;
        //private readonly UserManager<IdentityUser> _userManager;

        public QuestionController(DbContextSetup context)
        {
            _context = context;
           
        }

        // GET: Question
        public IActionResult Index()
        {
            var questions = _context.Questions.Include(q => q.Options).ToList();
            ViewBag.Questions = questions;
            return View();
            //return View(questions);
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            var exams = _context.Exams.ToList();
            ViewBag.Exams = exams;
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Question question)
        {
           
            if (ModelState.IsValid)
            {
                //var user = await _userManager.GetUserAsync(User);
                //Console.WriteLine(user.UserName);
                //var userId = user.Id;
                _context.Add(question);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Question/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = _context.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
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
            return View(question);
        }

        // GET: Question/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = _context.Questions
                .FirstOrDefault(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (QuestionExists(id)){
                var question = _context.Questions.Find(id);
                _context.Questions.Remove(question);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
