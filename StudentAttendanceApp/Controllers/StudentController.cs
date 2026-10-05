using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentAttendanceApp.Models;
using StudentAttendanceApp.Repositories;

namespace StudentAttendanceApp.Controllers
{
    public class StudentController : Controller
    {
        StudentRepository repo = new StudentRepository();

        public async Task<IActionResult> Index()
        {
            List<Student> list = await repo.GetAllStudents();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student s)
        {
            await repo.AddStudent(s);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Student s = await repo.GetStudentById(id);
            return View(s);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student s)
        {
            await repo.UpdateStudent(s);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
