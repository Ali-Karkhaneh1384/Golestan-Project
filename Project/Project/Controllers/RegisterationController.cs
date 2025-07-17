using Golestan_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Project.Controllers
{
    public class RegisterationController : Controller
    {
        private readonly GolestanDbContext _dbContext;

        public RegisterationController(GolestanDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult WhichRole()
        {
            ViewBag.RoleList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Student", Text = "دانشجو" },
                new SelectListItem { Value = "Instructor", Text = "استاد" }
            };

            return View();
        }
        [HttpPost]
        public IActionResult WhichRole(string selectedRole)
        {
            if (selectedRole == "Instructor")
                return RedirectToAction("WhichInstructor", "Registeration");
            else if (selectedRole == "Student")
                return RedirectToAction("WhichStudent", "Registeration");
            else
                return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> WhichInstructor()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var instructors = await _dbContext.instructors.Where(i => i.user_id == userId).ToListAsync();
            if (instructors.Count() < 2) return RedirectToAction("Index", "Instructor");
            ViewBag.students = instructors.Select(x => new SelectListItem
            {
                Value = x.instructor_id.ToString(),
                Text = "Instructor ID : " + x.instructor_id
            });
            return View();
        }

        [HttpPost]
        public IActionResult WhichInstructor(string selectedInstructorId)
        {
            HttpContext.Session.SetInt32("InstructorId", int.Parse(selectedInstructorId));
            return RedirectToAction("Index", "Instructor");
        }

        public async Task<IActionResult> WhichStudent()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var students = await _dbContext.students.Where(i => i.user_id == userId).ToListAsync();
            if (students.Count() < 2) return RedirectToAction("Index", "Student");
            ViewBag.students = students.Select(x => new SelectListItem
            {
                Value = x.student_id.ToString(),
                Text = "Student ID : " + x.student_id
            });
            return View();
        }

        [HttpPost]
        public IActionResult WhichStudent(string selectedStudentId)
        {
            HttpContext.Session.SetInt32("StudentId", int.Parse(selectedStudentId));
            return RedirectToAction("Index", "Student");
        }
    }
}
