using WebShopProject.Models;
namespace WebShopProject.Data
{
    public class FnHelper
    {

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
    }
}
