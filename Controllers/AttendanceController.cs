using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

public class AttendanceController : Controller
{
    private readonly AppDbContext _db;

    public AttendanceController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Attendance
    public IActionResult Index(string filterDate)
    {
        var list = _db.Attendances
            .Include(a => a.Student)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filterDate))
        {
            DateTime date = DateTime.Parse(filterDate);
            list = list.Where(a => a.Date.Date == date.Date);
        }

        ViewBag.FilterDate = filterDate;
        ViewBag.Present = list
            .Count(a => a.Status == "Present");
        ViewBag.Absent = list
            .Count(a => a.Status == "Absent");
        ViewBag.Late = list
            .Count(a => a.Status == "Late");

        return View(list
            .OrderByDescending(a => a.Date)
            .ToList());
    }

    // GET: /Attendance/Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Students = _db.Students
                            .OrderBy(s => s.FullName)
                            .ToList();
        return View();
    }

    // POST: /Attendance/Create
    [HttpPost]
    public IActionResult Create(Attendance attendance)
    {
        ViewBag.Students = _db.Students
                         .OrderBy(s => s.FullName)
                         .ToList();

        _db.Attendances.Add(attendance);
        _db.SaveChanges();
        TempData["Success"] = "Attendance recorded!";
        return RedirectToAction("Index");
    }

    // Delete
    public IActionResult Delete(int id)
    {
        var att = _db.Attendances.Find(id);
        if (att != null)
        {
            _db.Attendances.Remove(att);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
