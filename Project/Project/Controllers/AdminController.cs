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
                instructors = _dbContext.instructors.Include(i => i.user).ToList(),
                courses = _dbContext.courses.ToList(),
                sections = _dbContext.sections
                    .Include(s => s.course)
                    .Include(s => s.classroom)
                    .ToList()
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
                Text = $"{x.day} ({x.start_time.ToString(@"hh\:mm")} - {x.end_time.ToString(@"hh\:mm")})"
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
                foreach(var x in section.TimeSlotIds)
                {
                    _dbContext.section_Times.Add(new section_time() { time_slot_id = x, section_id = section.Id });
                }
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("CreateTeach", section);
            }
            return RedirectToAction("Failure");
        }
        public IActionResult CreateTeach(sections sc)
        {
            ViewBag.InstructorList = _dbContext.instructors.Include(i => i.user).Select(x => new SelectListItem
            {
                Value = x.instructor_id.ToString(),
                Text = $"{x.user.First_Name} {x.user.Last_Name} ({x.user.Email})"
            });
            ViewBag.SectionID = sc.Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeach(SendSectionId model)
        {
            _dbContext.teaches.Add(new teach { instructor_id =  model.InstructorId , section_id = model.SectionId});
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Success");
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
        public async Task<IActionResult> DeleteCourse(int CourseId)
        {
            var deletedCourse = await _dbContext.courses.FindAsync(CourseId);
            if (deletedCourse == null) return RedirectToAction("Failure");

            _dbContext.courses.Remove(deletedCourse);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSection(int sectionId)
        {
            var deletedSection = await _dbContext.sections.FindAsync(sectionId);
            if (deletedSection != null) return RedirectToAction("Failure");

            _dbContext.sections.Remove(deletedSection);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int StudentId)
        {
            var DeleteStudent = await _dbContext.students.FindAsync(StudentId);
            if (DeleteStudent == null) return RedirectToAction("Failure");

          
            var userRole = await _dbContext.user_roles.FirstOrDefaultAsync(ur => ur.UserId == DeleteStudent.user_id && ur.RoleId == 3);
            if (userRole != null)
            {
                _dbContext.user_roles.Remove(userRole);
            }

           
            _dbContext.students.Remove(DeleteStudent);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteInstructor(int InstructorId)
        {
            var DeleteInstructor = await _dbContext.instructors.FindAsync(InstructorId);
            if (DeleteInstructor == null) return RedirectToAction("Failure");

            var userRole = await _dbContext.user_roles.FirstOrDefaultAsync(ur => ur.UserId == DeleteInstructor.user_id && ur.RoleId == 2);
            if (userRole != null)
            {
                _dbContext.user_roles.Remove(userRole);
            }



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
