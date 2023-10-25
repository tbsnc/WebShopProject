using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;
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
        private readonly FnHelper _fnHelper;
        public AdminUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _fnHelper = new FnHelper(context, userManager,roleManager);
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

            user.UserRole = _fnHelper.GetUserRole(id);

            return View(user);
        }

        public IActionResult Edit(string? id,string? message)
        {
            if (message != null) ViewBag.Message = message;
            var user = _context.Users.Find(id);
            //user.UserRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            ViewBag.UserRoles = _roleManager.Roles;
            
            user.UserRole = _fnHelper.GetUserRole(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            ModelState.Remove("Order");
            if(ModelState.IsValid)
            {
                var u = await _userManager.FindByIdAsync(user.Id);

                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Address = user.Address;
                u.City = user.City;
                u.PostalCode = user.PostalCode;
                u.Country = user.Country;

                var userRole = await _userManager.GetRolesAsync(u);

                if (userRole.Count > 0 && (userRole[0] != user.UserRole))
                {
                    
                    await _userManager.RemoveFromRolesAsync(u, new List<string> { userRole[0] });
                    await _userManager.AddToRoleAsync(u, user.UserRole);
                }

                await _userManager.UpdateAsync(u);
                
                return RedirectToAction("Edit",new { id = user.Id, message = "Changes saved."});
                
            }
           
            ViewBag.UserRoles = _roleManager.Roles;
            return View(user);
        }


        public IActionResult Delete(string? id)
        {
            if (id == null) return RedirectToAction("Index", new { error = "User not found" });


            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            if (user == null) return RedirectToAction("Index", new { error = "User not found" });
            user.UserRole = _fnHelper.GetUserRole(user.Id);

            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(string? id)
        {
            if (id == null) return RedirectToAction("Index", new { error = "User not found" });
            
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _userManager.DeleteAsync(user);
            }else
            {
                return RedirectToAction("Index", new { error = "User not found" });
            }

            return RedirectToAction("Index", new { message = $"User {user.UserName} deleted" }); ;

        }

    }
}
