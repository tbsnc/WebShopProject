using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;
using WebShopProject.Constants;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly FnHelper _fnHelper;
        public AdminUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IUserStore<ApplicationUser> userStore)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _fnHelper = new FnHelper(context, userManager, roleManager);
            _signInManager = signInManager;
            _userStore = userStore;
        }

        public IActionResult Index(string? message, string? error)
        {
            if (_userManager == null || _userManager.Users == null)
            {
                ViewBag.Error = "No users in DB.";
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
                return RedirectToAction("Index",new { error = "User not found." });
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
            ModelState.Remove("Password");
            ModelState.Remove("PasswordConfirmed");
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
            if (id == null) return RedirectToAction("Index", new { error = "User not found." });


            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            if (user == null) return RedirectToAction("Index", new { error = "User not found." });
            user.UserRole = _fnHelper.GetUserRole(user.Id);

            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (id == null) return RedirectToAction("Index", new { error = "User not found." });
            
            var user = _context.Users.Find(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }else
            {
                return RedirectToAction("Index", new { error = "User not found." });
            }

            return RedirectToAction("Index", new { message = $"User {user.UserName} deleted." }); ;

        }

        public IActionResult CreateUser()
        {
            
            ViewBag.UserRoles = _roleManager.Roles;
            

            //var user = _signInManager.ge;
           // user.UserRole = "User";
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> CreateUser(ApplicationUser user)
        {
            ModelState.Remove("Order");
            if (ModelState.IsValid)
            {
                if (user.Password != user.PasswordConfirmed)
                {
                    ModelState.AddModelError("Password", "Passwords do not match");
                    ViewBag.UserRoles = _roleManager.Roles;
                    return View(user);
                }
                var newUser = new ApplicationUser()
                {
                    UserName = user.Email,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    City = user.City,
                    PostalCode = user.PostalCode,
                    Country = user.Country,
                    EmailConfirmed = true, 
                    PhoneNumberConfirmed = true,
                };


                var userExists = await _userManager.FindByEmailAsync(newUser.Email);
                if (userExists == null)
                {

                    var result = await _userManager.CreateAsync(newUser, user.Password);

                    if(result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, user.UserRole);
                    }
                    else
                    { //probably password error
                        ViewBag.UserRoles = _roleManager.Roles; 
                        List<string> errorList = new List<string>();
                        foreach (var item in result.Errors)
                        {
                            errorList.Add(item.Description);
                        }
                        ViewBag.Error = errorList;
                        return View(user);
                    }
                    
                    

                    return RedirectToAction("Index", new { message = $"User - {user.Email} Created!" });
                }
                else
                {
                    ViewBag.Error = "Email taken!";
                    ViewBag.UserRoles = _roleManager.Roles;
                    return View(user);
                }



            }

            ViewBag.UserRoles = _roleManager.Roles;
            return View(user);
        }

    }
}
