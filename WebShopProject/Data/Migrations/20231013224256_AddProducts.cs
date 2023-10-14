using Humanizer;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.Metrics;
using WebShopProject.Models;

#nullable disable

namespace WebShopProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            List<Product> products = new List<Product>()
            {
                new Product { Name = "Fender Vintage II 1961 Stratocaster", Id = 1,
                    ProductCategory = new List<ProductCategory>(){ new ProductCategory() {CategoryId = 1, CategoryName = "Electric"} },
                    Description= "Solidbody Electric Guitar with Alder Body, Maple Neck, Rosewood Fingerboard, and 3 Single-coil Pickups - Olympic White",
                    ProductImage = new List<ProductImage>() { new ProductImage { Name = "nder Vintage II" , FileName = "/images/products/1/fender_vintage_II.jpeg" } }  ,Quantity = 50, Price = 1999.99M},


                new Product { Name = "Gibson Les Paul Standard", Id = 2,
                    ProductCategory = new List<ProductCategory>(){ new ProductCategory() { CategoryId = 1,CategoryName = "Electric"} },
                    Description= "Solidbody Electric Guitar with Mahogany Body, Maple Top, Mahogany Neck, Rosewood Fingerboard, and 2 Humbucking Pickups - Gold Top",
                    ProductImage = new List < ProductImage >() { new ProductImage { Name = "Gibson Les Paul", FileName = "/images/products/2/gibson_les_paul_standard.jpeg" } }, Quantity = 5, Price = 2999.99M},

                new Product { Name = "Kala Solid", Id = 3, 
                    ProductCategory = new List<ProductCategory>(){ new ProductCategory() {CategoryId = 2, CategoryName = "Ukulele"} },
                    Description= "8-string Baritone Ukulele with Cedar Top, Acacia Back and Sides, MahoGany Neck, and Rosewood Fingerboard - Natural",
                    ProductImage = new List<ProductImage>() { new ProductImage { Name = "Kala Solid", FileName = "/images/products/3/kala_solid_cedar.jpeg" } }, Quantity = 10, Price = 299.99M},


                new Product { Name = "Taylor GS Mini-e",Id=4, 
                    ProductCategory = new List<ProductCategory>(){ new ProductCategory() { CategoryId= 3, CategoryName = "Acoustic"} },
                    Description= "6-string Acoustic-electric Guitar with Tropical Mahogany Top, Layered Sapele Back and Sides, and Ebony Fingerboard - Natural",
                    ProductImage = new List < ProductImage >() { new ProductImage { Name = "Taylor GS Mini-e", FileName = "/images/products/4/Taylor_GS_Mini-e.jpeg" } }, Quantity = 25, Price = 699M},

                new Product { Name = "Yamaha  C40II",Id=5, 
                    ProductCategory = new List<ProductCategory>(){ new ProductCategory() { CategoryId=3, CategoryName = "Acoustic" } },
                    Description= "Nylon-string Acoustic Guitar with Spruce-Top, Meranti Back and Sides, Nato Neck, and Rosewood Fingerboard and Bridge - Natural",
                    ProductImage = new List < ProductImage >() { new ProductImage { Name = "Yamaha C40II", FileName = "/images/products/5/yamahaC40II.jpeg" } }, Quantity = 5, Price = 600M}

            };



            //List<ProductImage> productImages = new List<ProductImage>()
            //{
            //    new ProductImage { ProductId = 1, Name = "Fender Vintage II", FileName = "/images/products/1/fender_vintage_II.jpeg", IsMainImage = true },
            //    new ProductImage { ProductId = 2, Name = "Gibson Les Paul", FileName = "/images/products/2/gibson_les_paul_standard.jpeg", IsMainImage = true },
            //    new ProductImage { ProductId = 3, Name = "Kala Solid", FileName = "/images/products/3/kala_solid_cedar.jpeg", IsMainImage = true },
            //    new ProductImage { ProductId = 4, Name = "Taylor GS Mini-e", FileName = "/images/products/4/Taylor_GS_Mini-e.jpeg", IsMainImage = true },
            //    new ProductImage { ProductId = 5, Name = "Yamaha C40II", FileName = "/images/products/5/yamahaC40II.jpeg", IsMainImage = true }
            //};

            migrationBuilder.Sql(@"
                ALTER TABLE ProductCategory DROP CONSTRAINT [FK_ProductCategory_Category_CategoryId]
                GO
                ALTER TABLE ProductCategory DROP CONSTRAINT [FK_ProductCategory_Product_ProductId]
                GO
                ALTER TABLE ProductImage DROP CONSTRAINT [FK_ProductImage_Product_ProductId]
                GO
                ");


            foreach (Product product in products)
            {
                migrationBuilder.Sql(@$"
                
                SET IDENTITY_INSERT Product ON
                INSERT INTO Product ([Id],[Name],[Description],[Active],[Quantity],[Price]) 
                VALUES ({product.Id},'{product.Name}','{product.Description}',1,
                {product.Quantity}, {product.Price.ToString().Replace(',', '.')})
                SET IDENTITY_INSERT Product OFF

                SET IDENTITY_INSERT Category ON
                IF NOT EXISTS (SELECT 1 FROM Category WHERE [Name] = '{product.ProductCategory.FirstOrDefault().CategoryName}')
	                INSERT INTO Category([Id],[Name]) VALUES ( {product.ProductCategory.FirstOrDefault().CategoryId},'{product.ProductCategory.FirstOrDefault().CategoryName}')
                SET IDENTITY_INSERT Category OFF

                INSERT INTO ProductCategory(CategoryId,ProductId) VALUES ({product.ProductCategory.FirstOrDefault().CategoryId},{product.Id})
              
                INSERT INTO ProductImage([Name],[FileName],[IsMainImage],[ProductId]) 
                VALUES ('{product.ProductImage.FirstOrDefault().Name}',
                            '{product.ProductImage.FirstOrDefault().FileName}', 1, {product.Id})
                ");
            }

            migrationBuilder.Sql(@"           
                ALTER TABLE [dbo].[ProductCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductCategory_Category_CategoryId] FOREIGN KEY([CategoryId])
                REFERENCES [dbo].[Category] ([Id])
                ON DELETE CASCADE
                GO             
                ALTER TABLE [dbo].[ProductCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductCategory_Product_ProductId] FOREIGN KEY([ProductId])
                REFERENCES [dbo].[Product] ([Id])
                ON DELETE CASCADE
                GO  
                ALTER TABLE[dbo].[ProductImage] WITH NOCHECK ADD CONSTRAINT[FK_ProductImage_Product_ProductId] FOREIGN KEY([ProductId])
                REFERENCES[dbo].[Product]([Id])
                ON DELETE CASCADE
                GO
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE ProductCategory");
            migrationBuilder.Sql("TRUNCATE TABLE ProductImage");
            migrationBuilder.Sql("TRUNCATE TABLE Products");
            migrationBuilder.Sql("TRUNCATE TABLE Category");
        }
    }
}
