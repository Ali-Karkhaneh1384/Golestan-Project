using BCrypt.Net;
using Golestan_Project.Data;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Golestan_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly GolestanDbContext _dbContext;

        public AccountController(GolestanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email , string password)
        {
            var user = await _dbContext.users.Include(x => x.Instructors).Include(x => x.students).FirstOrDefaultAsync( user => user.Email == email );
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Hashed_Password))
            {
                ModelState.AddModelError("","ایمیل یا رمز عبور اشتباه است!");
                return View();
            }
            
            var roles = await _dbContext.user_roles.Where(userRole => userRole.UserId == user.Id).Join(_dbContext.roles , ur => ur.RoleId , r => r.Id , (ur , r)=> r.name.ToString()).ToListAsync();

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim (ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Name , $"{user.First_Name} {user.Last_Name}")
            };
            foreach(var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var Identity= new ClaimsIdentity(claim , CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(Identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Admin");
            else if (roles.Count > 1)
                return RedirectToAction("WhichRole", "Registeration");
            else if (roles.Contains("Instructor"))
                return RedirectToAction("WhichInstructor", "Registeration");
            else
                return RedirectToAction("WhichStudent", "Registeration");


        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
