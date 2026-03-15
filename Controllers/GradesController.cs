using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

public class GradesController : Controller
{
    private readonly AppDbContext _db;

    public GradesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Grades
    public IActionResult Index()
    {
        var grades = _db.Grades
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .OrderBy(g => g.Student.FullName)
            .ToList();
        return View(grades);
    }

    // GET: /Grades/Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Students = _db.Students
                          .OrderBy(s => s.FullName)
                          .ToList();
        ViewBag.Subjects = _db.Subjects
                              .OrderBy(s => s.SubjectName)
                              .ToList();
        return View();
    }

    // POST: /Grades/Create
    [HttpPost]
    public IActionResult Create(Grade grade)
    {
        ViewBag.Students = _db.Students
                         .OrderBy(s => s.FullName)
                         .ToList();
        ViewBag.Subjects = _db.Subjects
                              .OrderBy(s => s.SubjectName)
                              .ToList();

        // Auto calculate grade letter
        grade.GradeLetter = grade.Score >= 90 ? "A"
            : grade.Score >= 80 ? "B+"
            : grade.Score >= 70 ? "B"
            : grade.Score >= 60 ? "C"
            : "F";

        _db.Grades.Add(grade);
        _db.SaveChanges();
        TempData["Success"] = "Grade added!";
        return RedirectToAction("Index");
    }

    // Delete
    public IActionResult Delete(int id)
    {
        var grade = _db.Grades.Find(id);
        if (grade != null)
        {
            _db.Grades.Remove(grade);
            _db.SaveChanges();
        }
        TempData["Success"] = "Grade deleted!";
        return RedirectToAction("Index");
    }
}