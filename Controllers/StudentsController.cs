using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        { 
            _db = db;
        }

        // GET: /Students - Show all Students
        public IActionResult index(string search)
        {
            var students = _db.Students.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                students = students
                    .Where(s => s.FullName.Contains(search)
                               || s.ClassRoom.Contains(search));
            ViewBag.Search = search;
            return View(students.OrderBy(s => s.FullName).ToList());
        }

        // GET: /Students/Details/1
        public IActionResult Details(int id)
        {
            var student = _db.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }

        // GET: /Students/Create
        [HttpGet]
        public IActionResult Create() { return View(); }
        // POST: /Students/Create
        [HttpPost]
        public IActionResult Create(Student student) 
        {
            if (ModelState.IsValid)
            { 
                _db.Students.Add(student);
                _db.SaveChanges();
                TempData["Success"] =
                    student.FullName + "added successfully!";
                return RedirectToAction("Index");
            }
            return View(student);
        }
        // GET: /Students/Edit/1
        public IActionResult Edit(int id)
        {
            var student = _db.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }
        // POST: /Students/Edit/1
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            { 
                _db.Students.Update(student);
                _db.SaveChanges();
                TempData["Success"] =
                    student.FullName + " updated successfully!";
                return RedirectToAction("Index");
            }
            return View(student);
        }
        //GET: /Students/Delete/1
        public IActionResult Delete(int id)
        {
            var student = _db.Students.Find(id);
            if (student == null) return NotFound();
            _db.Students.Remove(student);
            _db.SaveChanges();
            TempData["Success"] = "Student deleted!";
            return RedirectToAction("Index");
            
        }
    }
}
