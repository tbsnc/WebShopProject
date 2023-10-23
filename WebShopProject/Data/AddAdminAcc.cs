using Microsoft.AspNetCore.Identity;
using WebShopProject.Constants;

namespace WebShopProject.Data
{
    public static class AddAdminAcc
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //seed roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));


            //create admin

            var user = new ApplicationUser()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin",
                Address = "AdminAddress",
                City = "AdminCity",
                PostalCode = "10000",
                Country = "AdminCountry",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var userExists = await userManager.FindByEmailAsync(user.Email);
            if (userExists == null)
            {
                await userManager.CreateAsync(user, "Admin123!");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }


    }
}
