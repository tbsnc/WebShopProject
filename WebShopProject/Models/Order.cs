using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebShopProject.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("OrderId")]
        public virtual ICollection<OrderItem> OrderItems { get; set;}

        public string UserId { get; set; }
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }
        
        #region Billing Info

        [Required(ErrorMessage = "First name is required!")]
        [StringLength(100,MinimumLength = 2)]
        public string BillingFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength (100,MinimumLength = 2)]
        public string BillingLastName { get; set; }

        [Required(ErrorMessage ="Email address is required!")]
        [StringLength(200)]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is not valid!")]
        public string BillingEmail{ get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string BillingPhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(200)]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(200)]
        public string BillingCity { get; set; }

        [Required(ErrorMessage = "Postal code is required!")]
        [DataType(DataType.PostalCode)]
        [StringLength(20)]
        public string BillingPostalCode { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [StringLength(100)]
        public string BillingCountry { get; set; }
        #endregion

        #region Shipping Info

        [Required(ErrorMessage = "First name is required!")]
        [StringLength(100, MinimumLength = 2)]
        public string ShippingFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(100, MinimumLength = 2)]
        public string ShippingLastName { get; set; }

        [Required(ErrorMessage = "Email address is required!")]
        [StringLength(200)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid!")]
        public string ShippingEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string ShippingPhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(200)]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(200)]
        public string ShippingCity { get; set; }

        [Required(ErrorMessage = "Postal code is required!")]
        [DataType(DataType.PostalCode)]
        [StringLength(20)]
        public string ShippingPostalCode { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [StringLength(100)]
        public string ShippingCountry { get; set; }
        #endregion

    }
}
