namespace WebShopProject.Models
{
    public class AppUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set;}

        public string Email { get; set;}

        public string Role { get; set;}

        public bool EmailConfirmed { get; set;}

        public bool TwoFactorAuthActive { get; set;}
    }
}
