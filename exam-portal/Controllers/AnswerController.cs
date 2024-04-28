using exam_portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace exam_portal.Controllers
{
    public class AnswerController : Controller
    {
        private readonly DbContextSetup _context;

        public AnswerController(DbContextSetup context)
        {
            _context = context;
        }
        

    }
}
