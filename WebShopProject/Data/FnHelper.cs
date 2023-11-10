using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Net.Mail;
using System.Security.Cryptography;
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
        /// Checks cart items and returns error list if found.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cartItem"></param>
        /// <returns>Error list</returns>
        public  List<string> VerifyCartProduct(List<CartItem> cart)
        {
            List<string> Error = new List<string>();
            
            for (int i = 0; i < cart.Count; i++)
            {
                Product product = _context.Product.Find(cart[i].Product.Id);

                if (product == null)
                {
                    cart.RemoveAt(i);
                    Error.Add($"Product - {cart[i].Product.Name} - was not found and was removed from cart.");
                    i--;
                }

                if (product.Quantity < cart[i].Product.Quantity)
                {
                    cart[i].Quantity = product.Quantity;
                    Error.Add($"Product - {product.Name} - quantity reduced to availiable amount.");
                }

                if (product.Quantity == 0)
                {
                    cart.RemoveAt(i);
                    Error.Add($"Product - {product.Name} - is out of stock and was removed from cart.");
                    i--;
                }
                if (!product.Active)
                {
                    cart.RemoveAt(i);
                    Error.Add($"Product - {product.Name} - is not availiable at this time.");

                }
            }

            
            return Error;
        }

        /// <summary>
        /// Shuffles Product List and returns 10 products
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public  List<Product> ShuffleProductList(List<Product> list) 
        { 
            Random rnd = new Random();
            
            int n = list.Count;

            if (list.Count >= 10)
            {
                List<Product> randomProducts = new List<Product>();

                while (list.Count > 0)
                {
                    int index = rnd.Next(0, list.Count);
                    randomProducts.Add(list[index]);
                    list.RemoveAt(index);
                    if (randomProducts.Count == 10) return randomProducts;
                }
            }
            return list;
        }
        /// <summary>
        /// Checks if email form is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailValid(string? email)
        {
            if(email == null) return false;

            if (email.Trim().EndsWith('.')) return false;

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch { return false; }

        }

        public List<Category> GetProductCategories(int? productId)
        {
            return (from category in _context.Category
                    join productCategory in _context.ProductCategory
                    on category.Id equals productCategory.CategoryId
                    where productCategory.ProductId == productId
                    select new Category()
                    {
                        Id = category.Id,
                        Name = category.Name
                    }).ToList();
        }

    }
}
