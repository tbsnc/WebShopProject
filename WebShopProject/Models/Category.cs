using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 2)]
        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }

    }
}
