using Microsoft.AspNetCore.Mvc;

namespace exam_portal.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult StudentDashboard()
        {
            return View();
        }

        public IActionResult TeacherDashboard()
        {
            return View();
        }
    }
}
