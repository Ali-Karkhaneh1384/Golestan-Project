using Golestan_Project.Data;
using Microsoft.AspNetCore.Identity;
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
        public IActionResult ShowStudentDetails(int studentId)
        {
            var student = _dbContext.students
                    .Include(s => s.users) 
                    .Include(s => s.takes)
                    .ThenInclude(t => t.sections)
                    .ThenInclude(s => s.course)
                    .ThenInclude(c => c.sections)
                    .ThenInclude(s => s.teach)
                    .ThenInclude(t => t.instructor)
                    .ThenInclude(i => i.user)
                    .FirstOrDefault(s => s.student_id == studentId);
            if (student == null)
            {
                return NotFound("دانشجو یافت نشد");
            }
            return View(student);
        }
        public IActionResult ShowInstructorDetails(int instructorId)
        {
            var instructor = _dbContext.instructors
                        .Include(x => x.user)
                        .Include(x => x.teaches)
                        .ThenInclude(x => x.section)
                        .ThenInclude(x => x.course)
                        .ThenInclude(x => x.sections)
                        .ThenInclude(x => x.classroom)
                        .FirstOrDefault(x => x.instructor_id == instructorId);
            if (instructor == null)
            {
                return NotFound("استاد یافت نشد");
            }
            return View(instructor);
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
        public IActionResult AddStudentToSection()
        {
            ViewBag.students = _dbContext.students.Select(s => new SelectListItem
            {
                Value = s.student_id.ToString(),
                Text = $"{s.users.First_Name} {s.users.Last_Name} ({s.users.Email})"
            }).ToList();

            ViewBag.sections = _dbContext.sections.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.course.Title} -  نیمسال {s.semester}"
            }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudentToSection([Bind("student_id, section_id, grade")] takes model)
        {
         
            ModelState.Remove("students");
            ModelState.Remove("sections");

            if (ModelState.IsValid)
            {
               
                if (await _dbContext.takes.AnyAsync(t => t.student_id == model.student_id && t.section_id == model.section_id))
                {
                    ModelState.AddModelError("", "این دانشجو قبلاً در این بخش ثبت‌نام کرده است.");
                    return View(model);
                }
                var SectionStudentNum = await _dbContext.takes.Where(x => x.section_id == model.section_id).CountAsync();
                var ModelSection = await _dbContext.sections.FirstOrDefaultAsync(x => x.Id == model.section_id);
                var modelTimeSlots = await _dbContext.section_Times.Where(x => x.section_id == ModelSection.Id).ToListAsync();
                var SectionClassroom = await _dbContext.classrooms.FirstOrDefaultAsync(x => x.Id == ModelSection.classroom_id);
                var ClassroomCapacity = SectionClassroom.capacity;
                if (ClassroomCapacity > SectionStudentNum)
                {
                    var AllStdTakes = await _dbContext.takes.Where(x => x.student_id == model.student_id).ToListAsync();
                    foreach(var take in AllStdTakes)
                    {
                        var Sec = await _dbContext.sections.FirstOrDefaultAsync(x => x.Id == take.section_id);
                        var timeSlots = await _dbContext.section_Times.Where(x => x.section_id == Sec.Id).ToListAsync();
                        foreach(var i in timeSlots)
                        {
                            foreach(var j in modelTimeSlots) 
                                if(j.time_slot_id == i.time_slot_id)
                                {
                                    ModelState.AddModelError("", "دانشجو در این زمان کلاس دیگری دارد");
                                    return View(model);
                                }
                        }
                    }
                    _dbContext.takes.Add(model);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError("", "ظرفیت این کلاس تکمیل است.");
                    return View(model);
                }

            }

            return RedirectToAction("Failure");
        }
        [HttpPost]
        public IActionResult DeleteStudentFromSection(int studentId, int sectionId)
        {
            var student = _dbContext.students
                .Include(s => s.takes)
                .FirstOrDefault(s => s.student_id == studentId);

            if (student == null)
            {
                return NotFound("دانش‌آموز یافت نشد.");
            }

            var takeToRemove = student.takes.FirstOrDefault(t => t.section_id == sectionId);
            if (takeToRemove != null)
            {
                _dbContext.takes.Remove(takeToRemove);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("ShowStudentDetails", new { studentId = studentId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteInstructorFromSection(int instructorId, int sectionId)
        {
            var instructor = await _dbContext.instructors
                .Include(x => x.teaches).FirstOrDefaultAsync(x => x.instructor_id == instructorId);
            if (instructor == null) return NotFound("استاد یافت نشد.");
            var teachToRemove = instructor.teaches.FirstOrDefault(x => x.section_id == sectionId);
            if (teachToRemove != null)
            {
                _dbContext.teaches.Remove(teachToRemove);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ShowInstructorDetails", new { instructorId = instructorId });
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
                if (await _dbContext.students.AllAsync(x => x.user_id == student.user_id))
                    _dbContext.user_roles.Add(new user_roles { UserId = student.user_id, RoleId = 3 });
                _dbContext.students.Add(student);
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
                if (await _dbContext.instructors.AllAsync(x => x.user_id == instructor.user_id))
                    _dbContext.user_roles.Add(new user_roles { UserId = instructor.user_id, RoleId = 2 });
                _dbContext.instructors.Add(instructor);
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
            if (deletedSection == null) return RedirectToAction("Failure");

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
