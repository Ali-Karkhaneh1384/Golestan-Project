using Golestan_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Project.Models;
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
        [HttpPost]
        public async Task<IActionResult> SubmitGrade(int NewGrade, int stdId, int secId)
        {
            var changedTake = await _dbContext.takes.FirstOrDefaultAsync(x => x.student_id == stdId && x.section_id == secId);
            if (changedTake == null)
            {
                ViewBag.error = "اطلاعات دانشجو یا کلاس یافت نشد.";
                return RedirectToAction("ShowSectionDetails", new { sectionId = secId });
            }
            changedTake.grade = NewGrade;
            await _dbContext.SaveChangesAsync();
            ViewBag.success = "نمره با موفقیت ثبت شد.";
            return RedirectToAction("ShowSectionDetails", new { sectionId = secId });
        }
        [HttpPost]
        public async Task<IActionResult> ResetGrade(int stdId, int secId)
        {
            var changedTake = await _dbContext.takes.FirstOrDefaultAsync(x => x.student_id == stdId && x.section_id == secId);
            if (changedTake == null)
            {
                ViewBag.error = "اطلاعات دانشجو یا کلاس یافت نشد.";
                return RedirectToAction("ShowSectionDetails", new { sectionId = secId });
            }
            changedTake.grade = null;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("ShowSectionDetails", new { sectionId = secId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudentFromSection(int studentId , int sectionId)
        {
            var student =  _dbContext.students.Include(s => s.takes).FirstOrDefault(s => s.student_id == studentId);
            if (student == null)
            {
                return NotFound("دانش‌آموز یافت نشد.");
            }

            var deletedTake = _dbContext.takes.FirstOrDefault(t => t.section_id == sectionId);
            if (deletedTake != null)
            {
                _dbContext.takes.Remove(deletedTake);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("ShowSectionDetails", new { sectionId = sectionId });
        }
    }
}
