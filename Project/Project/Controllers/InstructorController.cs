using Golestan_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class InstructorController : Controller
    {
        private readonly GolestanDbContext _dbContext;

        public InstructorController(GolestanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            int? instructorId = HttpContext.Session.GetInt32("InstructorId");
            var instructor = _dbContext.instructors
                .Include(i => i.teaches)
                .ThenInclude(t => t.section)
                .ThenInclude(s => s.classroom)
                .ThenInclude(c => c.sections)
                .ThenInclude(s => s.course)
                .ThenInclude(c => c.sections)
                .ThenInclude(s => s.section_Times)
                .ThenInclude(s => s.time_slot)
                .ThenInclude(t => t.section_Times)
                .ThenInclude(s => s.section)
                .Include(i => i.user)
                .FirstOrDefault(i => i.instructor_id == instructorId);

            if (instructor == null)
            {
                return NotFound("استاد یافت نشد");
            }
                
            return View(instructor);
        }
        public async Task<IActionResult> ShowSectionDetails(int sectionId)
        {
            var section = _dbContext.sections
                .Include(s => s.classroom)
                .ThenInclude(c => c.sections)
                .ThenInclude(s => s.course)
                .ThenInclude(c => c.sections)
                .ThenInclude(s => s.section_Times)
                .ThenInclude(s => s.time_slot)
                .ThenInclude(s => s.section_Times)
                .ThenInclude(s => s.section)
                .ThenInclude(s => s.teach)
                .ThenInclude(t => t.instructor)
                .ThenInclude(i => i.user)
                .Include(s => s.takes)
                .ThenInclude(t => t.students)
                .ThenInclude(s => s.users)
                .FirstOrDefault(s => s.Id == sectionId);
            if (section == null)
            {
                return NotFound("کلاس درس یافت نشد");
            }
            return View(section);
                
        }

    }
}
