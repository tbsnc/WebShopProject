using WebShopProject.Models;

namespace WebShopProject.Extensions
{
    public class CartItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerProduct { get; set; }
        public decimal GetTotalPrice()
        {
            return Product.Price * Quantity;
        }
    }
}
