using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(20)]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [ForeignKey("UserId")]

        public virtual ICollection<Order> Order { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

    }
}