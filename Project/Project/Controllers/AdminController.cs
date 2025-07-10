using Golestan_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
