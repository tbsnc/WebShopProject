using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebShopProject.Data;
using WebShopProject.Models;

namespace WebShopProject.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(string? message, string? error)
        {
            if (_userManager == null || _userManager.Users == null)
            {
                ViewBag.Error = "No users in DB";
                return View(new AppUserViewModel());
            }
            
            if(message != null) ViewBag.Message = message;
            if(error != null) ViewBag.Error = error;    

            var users = _userManager.Users.Select(x => new AppUserViewModel()
            {
                UserId = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                TwoFactorAuthActive = x.TwoFactorEnabled,
                EmailConfirmed = x.EmailConfirmed,
                Role = string.Join(',',_userManager.GetRolesAsync(x).Result.ToArray())
            });


            return View(users);
        }


        public  IActionResult Details(string? id) 
        {
            //check if user is auth and exists
            ApplicationUser user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index",new { error = "User not found"});
            }

            user.UserRole =  _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            return View(user);
        }

        public IActionResult Edit(string? id)
        {
            var user = _context.Users.Find(id);
            user.UserRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            ViewBag.UserRoles = _roleManager.Roles;
            return View(user);
        }
    }
}
