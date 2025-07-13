using Golestan_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class AdminController : Controller

    {
        private readonly GolestanDbContext _dbContext;

        public AdminController(GolestanDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.db = new
            {
                students = _dbContext.students.Include(s => s.users).ToList(),
                instructors = _dbContext.instructors.Include(i => i.user).ToList()
            };
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([Bind("First_Name, Last_Name, Email, Hashed_Password")] users model)
        {
            if(ModelState.IsValid)
            {
                model.Created_at = DateTime.Now;
                _dbContext.users.Add(model);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return RedirectToAction("Failure");
        }
        public IActionResult CreateCourse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(courses course)
        {
            if (ModelState.IsValid)
            {
                _dbContext.courses.Add(course);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return RedirectToAction("Failure");
        }
        public IActionResult CreateSection()
        {
            ViewBag.CourseList = _dbContext.courses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Title} ({x.code})"
            });
            ViewBag.ClassroomList = _dbContext.classrooms.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.building} - class: {x.room_number} ({x.capacity})"
            });
            ViewBag.TimeSlotList = _dbContext.timeslots.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.day} ({x.start_time - x.end_time})"
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSection(sections section)
        {
            if(ModelState.IsValid)
            {
                _dbContext.sections.Add(section);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("CreateTeach");
            }
            return RedirectToAction("Failure");
        }
        public IActionResult CreateTeach()
        {
            ViewBag.InstructorList = _dbContext.instructors.Include(i => i.user).Select(x => new SelectListItem
            {
                Value = x.instructor_id.ToString(),
                Text = $"{x.user.First_Name} {x.user.Last_Name} ({x.user.Email})"
            });
            return View();
        }
        public IActionResult SetRoleStudent()
        {
            ViewBag.UsersList = _dbContext.users.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.First_Name} {x.Last_Name} ({x.Email})"
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SetRoleStudent([Bind("user_id")]students student)
        {
            student.enrollment_date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _dbContext.students.Add(student);
                _dbContext.user_roles.Add(new user_roles { UserId = student.user_id, RoleId = 3 });
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return RedirectToAction("Failure");
        }
        public IActionResult SetRoleInstructor()
        {
            ViewBag.UsersList = _dbContext.users.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.First_Name} {x.Last_Name} ({x.Email})"
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SetRoleInstructor( instructors instructor)
        {
            instructor.hire_date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _dbContext.instructors.Add(instructor);
                _dbContext.user_roles.Add(new user_roles { UserId = instructor.user_id, RoleId = 2 });
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return RedirectToAction("Failure");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int StudentId)
        {
            var DeleteStudent = await _dbContext.students.FindAsync(StudentId);
            if (DeleteStudent == null) return RedirectToAction("Failure");
            _dbContext.students.Remove(DeleteStudent);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteInstructor(int InstructorId)
        {
            var DeleteInstructor = await _dbContext.instructors.FindAsync(InstructorId);
            if (DeleteInstructor == null) return RedirectToAction("Failure");
            _dbContext.instructors.Remove(DeleteInstructor);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Failure()
        {
            return View();
        }
    }
}
