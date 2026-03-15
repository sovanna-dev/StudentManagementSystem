using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

public class DashboardController : Controller
{
    private readonly AppDbContext _db;

    public DashboardController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        // Summary stats
        ViewBag.TotalStudents = _db.Students.Count();
        ViewBag.TotalSubjects = _db.Subjects.Count();
        ViewBag.TotalGrades = _db.Grades.Count();
        ViewBag.TotalPresent = _db.Attendances
            .Count(a => a.Status == "Present");

        // Top students by average score
        ViewBag.TopStudents = _db.Grades
            .Include(g => g.Student)
            .GroupBy(g => g.Student.FullName)
            .Select(g => new {
                Name = g.Key,
                Average = g.Average(x => x.Score)
            })
            .OrderByDescending(x => x.Average)
            .Take(5)
            .ToList();

        // Grade distribution
        ViewBag.GradeA = _db.Grades
            .Count(g => g.GradeLetter == "A");
        ViewBag.GradeBP = _db.Grades
            .Count(g => g.GradeLetter == "B+");
        ViewBag.GradeB = _db.Grades
            .Count(g => g.GradeLetter == "B");
        ViewBag.GradeC = _db.Grades
            .Count(g => g.GradeLetter == "C");
        ViewBag.GradeF = _db.Grades
            .Count(g => g.GradeLetter == "F");

        // Class distribution
        ViewBag.ClassA = _db.Students
            .Count(s => s.ClassRoom == "CS-A");
        ViewBag.ClassB = _db.Students
            .Count(s => s.ClassRoom == "CS-B");
        ViewBag.ClassC = _db.Students
            .Count(s => s.ClassRoom == "CS-C");

        return View();
    }
}
