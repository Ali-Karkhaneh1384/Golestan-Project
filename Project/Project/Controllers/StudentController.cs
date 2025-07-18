using Golestan_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class StudentController : Controller
    {
        private readonly GolestanDbContext _dbContext;
        public StudentController (GolestanDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");
            var student = _dbContext.students
                .Include(s => s.takes)
                .ThenInclude(t => t.sections)
                .ThenInclude(s => s.classroom)
                .ThenInclude(c => c.sections)
                .ThenInclude(s => s.course)
                .ThenInclude(c => c.sections)
                .ThenInclude(s => s.section_Times)
                .ThenInclude(s => s.time_slot)
                .ThenInclude(t => t.section_Times)
                .ThenInclude(s => s.section)
                .ThenInclude(s => s.teach)
                .ThenInclude(t => t.instructor)
                .ThenInclude(i => i.user)
                .Include(s => s.users)
                .FirstOrDefault(s => s.student_id == studentId);
            if (student == null)
                return NotFound("دانش آموز یافت نشد.");

            return View(student);
         
        }
    }
}
