using System.ComponentModel.DataAnnotations;

namespace WebShopProject.Data
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required,StringLength(10)]
        public string Alpha3 { get; set; }

        [Required, StringLength(10)]
        public string Alpha2 { get; set; }

        [Required, StringLength(10)]
        public string Code { get; set; }  

    }
}
