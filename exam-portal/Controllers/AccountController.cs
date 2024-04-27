namespace exam_portal.Controllers;
using exam_portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class AccountController : Controller
{
    private readonly DbContextSetup _context;

    public AccountController(DbContextSetup context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            // Redirect to appropriate action based on role
            if (user.Role == "Teacher")
            {
                return RedirectToAction("TeacherDashboard", "Dashboard");
            }
            else if (user.Role == "Student")
            {
                return RedirectToAction("StudentDashboard","Dashboard");
            }
            else
            {
                // Handle other roles or invalid scenarios
                return RedirectToAction("Index");
            }
        }
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignUp(string username, string password, string email, string name, string role)
    {
        _context.Users.Add(new User { Username = username, Password = password, Email = email, Name = name, Role = role });
        _context.SaveChanges();
        return RedirectToAction("Login");
    }
}
