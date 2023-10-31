using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using WebShopProject.Extensions;
using WebShopProject.Models;
namespace WebShopProject.Data
{
    public class FnHelper
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser>? _userManager;
        private readonly RoleManager<IdentityRole>? _roleManager;
        public FnHelper(ApplicationDbContext context, UserManager<ApplicationUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public FnHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetUserRole(string? userId)
        {
            return (from roles in _roleManager.Roles
                    join userRole in _context.UserRoles
                    on roles.Id equals userRole.RoleId
                    where userRole.UserId == userId
                    select roles.Name).FirstOrDefault();
        }


        /// <summary>
        /// Returns all items that are related to the order with product names
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
        /// Returns OrderItem with product name
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns></returns>
        public OrderItem GetOrderItemName(OrderItem orderItem) 
        {
            orderItem.ProductName = _context.Product.Where(x => x.Id == orderItem.ProductId).FirstOrDefault().Name;
            return orderItem;
        }



        /// <summary>
        /// Saves image localy and returns image that is ready for saving to DB 
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
