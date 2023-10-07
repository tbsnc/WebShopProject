using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebShopProject.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200,MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        [Column(TypeName = "bit")]
        public bool IsMainImage { get; set; }

        [Required]
        [StringLength (1000,MinimumLength = 2)]
        [DataType(DataType.ImageUrl)]
        public string FileName { get; set; }

        [NotMapped]
        public string ProductName { get; set; }


    }
}
