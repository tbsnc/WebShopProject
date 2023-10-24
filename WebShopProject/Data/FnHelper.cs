using Microsoft.Identity.Client;
using WebShopProject.Extensions;
using WebShopProject.Models;
namespace WebShopProject.Data
{
    public class FnHelper
    {
        private readonly ApplicationDbContext _context;
        
        public FnHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all items that are related to the order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public  List<OrderItem> GetOrderItems(int? orderId)
        {
            List<OrderItem> orderItems = (from ordItem in _context.OrderItem
                                          join product in _context.Product
                                          on ordItem.ProductId equals product.Id
                                          where ordItem.OrderId == orderId
                                          select new OrderItem()
                                          {
                                              Id = ordItem.Id,
                                              OrderId = ordItem.OrderId,
                                              ProductId = ordItem.ProductId,
                                              Quantity = ordItem.Quantity,
                                              Price = ordItem.Price,
                                              ProductName = product.Name
                                          }).ToList();
            return orderItems;
        }

        /// <summary>
        /// Saves image localy  
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="productId"></param>
        /// <param name="IsMainImage"></param>
        /// <returns>ProductImage</returns>
        public static async Task<ProductImage> CreateProductImage(IFormFile? imageFile, int productId, bool IsMainImage = true)
        {
            ProductImage productImage = new ProductImage();

            var uploadPath = Path.Combine("wwwroot", "images", "products", productId.ToString());

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            if (imageFile != null)
            {
                var fileName = Path.Combine(uploadPath, imageFile.FileName);
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                fileName = fileName.Replace("wwwroot\\", "/").Replace("\\", "/");

                productImage.FileName = fileName;
                productImage.IsMainImage = IsMainImage;
                productImage.ProductId = productId;
                productImage.Name = imageFile.FileName;
            }
            return productImage;
        }

        /// <summary>
        /// Checks if product is ready to be ordered 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cartItem"></param>
        /// <returns>Error list</returns>
        public static List<string> VerifyProduct(Product product, CartItem cartItem, out bool remove)
        {
            List<string> Error = new List<string>();
            remove = false;
            
            if(product == null) 
            {
                Error.Add($"Product - \"{product.Name}\" was not found and was removed from cart");
                remove = true;
            }

            if(product.Quantity < cartItem.Quantity) 
            {
                Error.Add("Product ");
                remove = false;
            }

            if (product.Quantity == 0)
            {
                Error.Add($"Product - \"{product.Name}\" is out of stock and was removed from cart");
                remove = true;
            }
            
            if (!product.Active)
            {
                Error.Add($"Product - \"{product.Name}\" not availiable and was removed from cart");
                remove = true;
            }
            
            
            
            return Error;
        }


    }
}
