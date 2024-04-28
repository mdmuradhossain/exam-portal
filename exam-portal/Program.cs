using exam_portal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DbContextSetup>();
/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                                       options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();*/

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
             name: "signup",
             pattern: "Signup",
             defaults: new { controller = "Account", action = "SignUp" });


app.MapControllerRoute(
              name: "student-dashboard",
               pattern: "{controller=Dashboard}/{action=StudentDashboard}");

app.MapControllerRoute(
              name: "teacher-dashboard",
               pattern: "{controller=Dashboard}/{action=TeacherDashboard}");


app.MapControllerRoute(
              name: "questions",
               pattern: "{controller=Question}/{action=Create}");

app.MapControllerRoute(
        name: "ViewQuestion",
        pattern: "Question/View",
        defaults: new { controller = "Question", action = "Index" });

app.MapControllerRoute(
              name: "exams",
              pattern: "{controller=Exam}/{action=Create}");
// defaults: new { controller = "Dashboard", action = "StudentDashboard" });
app.MapControllerRoute(
        name: "ViewExam",
        pattern: "Exam/List",
        defaults: new { controller = "Exam", action = "Index" });

app.MapControllerRoute(
    name: "SignleViewExam",
    pattern: "Exam/View/{id}",
    defaults: new { controller = "Exam", action = "View" }
);

app.MapControllerRoute(
    name: "ExamViewExam",
    pattern: "ExamView/{id}",
    defaults: new { controller = "ExamView", action = "Index" }
);

app.MapControllerRoute(
    name: "ExamViewExam",
    pattern: "Exam/Answers",
    defaults: new { controller = "ExamView", action = "Answers" }
);

app.Run();
