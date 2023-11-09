using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebShopProject.Data;
using WebShopProject.Models;

namespace WebShopProject.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FnHelper _fnHelper;
        public AdminProductController(ApplicationDbContext context)
        {
            _context = context;
            _fnHelper = new FnHelper(context);
        }

        // GET: AdminProduct
        public async Task<IActionResult> Index(string? message)
        {
            if(message != null) ViewBag.Message = message;  
            List<Product> products = _context.Product.ToList();

            if (products == null) return View("Index",new List<Product>());

            foreach (var item in products)
            {
                item.ProductImage = _context.ProductImage.Where(x => x.ProductId == item.Id).ToList();
                item.ProductCategory = _context.ProductCategory.Where(x => x.ProductId == item.Id).ToList();
                if (item.Description.Length > 50)
                {
                    item.Description = item.Description.Truncate(50);
                }
            }


            return View(products);
        }

        // GET: AdminProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: AdminProduct/Create
        public IActionResult Create()
        {
            
            Product product = new Product();
            if (_context.ProductCategory != null)
            {
                product.Category = _context.Category.ToList();
            }
            return View(product);
        }

        // POST: AdminProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string? chkDefaultImage,string? chkCategory,int? selectCategoryId, [Bind("Id,Name,Description,Active,Quantity,Price")] Product product)
        {
            ModelState.Remove("OrderItem");
            ModelState.Remove("Category");
           
            if (chkDefaultImage == "on" || HttpContext.Request.Form.Files.Count > 0) ModelState.Remove("ProductImage");
            if (chkCategory == "on")
            {
                ModelState.Remove("ProductCategory");
                ModelState.Remove("selectCategoryId");
            }
            else if(selectCategoryId == null)
            {
                ModelState.Remove("ProductCategory");
                ModelState.AddModelError("ProductCategory", "Category not selected!");
            }else
            { ModelState.Remove("ProductCategory"); }

            if (product.Quantity == 0)
            {//allow adding product with quantity 0
            
                ModelState.Remove("Quantity");
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);

                await _context.SaveChangesAsync();

                if (product.Id > 0 && HttpContext.Request.Form.Files.Count > 0)
                {
                    var imageFile = HttpContext.Request.Form.Files.FirstOrDefault();

                    ProductImage productImage =  await FnHelper.CreateProductImage(imageFile, product.Id);
                    
                    _context.Add(productImage);
                    _context.SaveChanges();
                }

                if (product.Id > 0 && selectCategoryId != null)
                {
                    ProductCategory productCategory = new ProductCategory()
                    {
                        CategoryId = (int)selectCategoryId,
                        CategoryName = _context.Category.FirstOrDefault(x => x.Id == selectCategoryId).Name,
                        ProductId = product.Id,
                        ProductName = product.Name

                    };

                    _context.Add(productCategory);
                    _context.SaveChanges();
                }



                return RedirectToAction(nameof(Index));
            }

            product.Category = _context.Category.ToList();
            return View(product);
        }
    

        // GET: AdminProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            foreach (var productImage in _context.ProductImage.ToList())
            {
                if(product.Id == productImage.ProductId)
                {
                    product.ProductImage.Add(productImage);
                }
            }

            product.Category = _fnHelper.GetProductCategories(product.Id);

            return View(product);
        }

        // POST: AdminProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Active,Quantity,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            ModelState.Remove("OrderItem");
            ModelState.Remove("ProductImage");
            ModelState.Remove("ProductCategory");
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: AdminProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: AdminProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> EditProductCategory(int? id, string? error)
        {
            if (id == null) 
                ViewBag.Error = ("Id is null.");

            if (error != null)
            {
                ViewBag.Error = error;
            }

            var product = _context.Product.Find(id);




            if (product != null)
            {
                product.ProductCategory = _context.ProductCategory.Where(x => x.ProductId == id).ToList();

                //add only categories which are not added to product
                var catId = product.ProductCategory.Where(x => x.ProductId == id).Distinct().ToList();
                product.Category = new List<Category>();
                foreach (var item in _context.Category)
                {
                    if(!catId.Any(x => x.CategoryId == item.Id))
                    {
                        product.Category.Add(item);
                    }
                }
            }

         

            foreach(var item in product.ProductCategory)
            {
                item.CategoryName = _context.Category.Where(x => x.Id == item.CategoryId).FirstOrDefault().Name;
            }

            return View(product);
        }

   
        public async Task<ActionResult> DeleteProductCategory(int? productId, int? categoryId)
        {
            if (productId == null) return Problem("Product Id is null");
            if (categoryId == null) return Problem("Category Id is null");

            var productCategory = _context.ProductCategory.Where(x => x.ProductId == productId &&  x.CategoryId == categoryId).FirstOrDefault();

            if(productCategory != null)
            {
                _context.ProductCategory.Remove(productCategory);
                await _context.SaveChangesAsync();
               return RedirectToAction("EditProductCategory", new { id = productId });
            }
            else
            {
                return RedirectToAction("Index", new { message = "Product category not found." });
            }

           
        }


        public async Task<ActionResult> AddProductCategory(int? productId, int? categoryId)
        {
            if (productId == null) return Problem("Product Id is null");
            if (categoryId == null) return Problem("Category Id is null");

            if (_context.ProductCategory.Where(x => x.ProductId == productId && x.CategoryId == categoryId).Count() == 0)
            {
                _context.ProductCategory.Add(new ProductCategory() { CategoryId = (int)categoryId, ProductId = (int)productId });
                await _context.SaveChangesAsync();
                return RedirectToAction("EditProductCategory", new { id = productId });
            }
            else
            {
                return RedirectToAction("EditProductCategory", new { id = productId, message = "Category already added to product." });
            }
            
            
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
