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

        public IActionResult Index()
        {
            if (_userManager == null || _userManager.Users == null)
            {
                ViewBag.Error = "No users in DB";
                return View(new AppUserViewModel());
            }
            
            var users = _userManager.Users.Select(x => new AppUserViewModel()
            {
                UserName = x.UserName,
                Email = x.Email,
                TwoFactorAuthActive = x.TwoFactorEnabled,
                EmailConfirmed = x.EmailConfirmed,
                Role = string.Join(',',_userManager.GetRolesAsync(x).Result.ToArray())
            });

            
                     

            return View(users);
        }
    }
}
