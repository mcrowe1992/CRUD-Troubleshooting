using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(context);
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(student, context);
                ViewData["Message"] = $"{student.Name} was added!";
                return RedirectToAction("Index");
            }
            // Show web page with errors
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            Student student = StudentDb.GetStudent(context, id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, student);
                ViewData["Message"] = "Student Updated!";
                return RedirectToAction("Index");
            }
            // Return view with errors
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            Student student = StudentDb.GetStudent(context, id);
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Student student = StudentDb.GetStudent(context, id);
            StudentDb.Delete(context, student);
            return RedirectToAction("Index");
        }

    }
}
