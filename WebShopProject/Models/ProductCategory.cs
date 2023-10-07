using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopProject.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        public string ProductName { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }    
    }
}
