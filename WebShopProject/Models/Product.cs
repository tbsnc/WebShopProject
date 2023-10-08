using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200,MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Active { get; set; }
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [NotMapped] 
        public bool UseDefaultImage { get; set; }


        [ForeignKey("ProductId")]
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }

        [ForeignKey("ProductId")]
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        [ForeignKey("ProductId")]
        public virtual ICollection<ProductImage> ProductImage { get; set; }

    }   
}
