using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShopProject.Data;
using WebShopProject.Models;

namespace WebShopProject.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminProductImageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminProductImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminProductImage
        public async Task<IActionResult> Index(int Id)
        {
        
            if (_context.ProductImage != null)
            {
                List<ProductImage> producImages = _context.ProductImage.ToList();
                if (producImages.Any(x => x.ProductId == Id))
                {
                    ViewBag.ProductId = Id;
                    return View(producImages.Where(x => x.ProductId == Id));
                }else
                {
                    ViewBag.ProductId = Id;
                    return View(new List<ProductImage>() );
                }
            }
            else
            {
                ViewBag.ProductId = Id;
                return View(new List<ProductImage>());
            }
              
              //return _context.ProductImage != null ? 
              //            View(await _context.ProductImage.ToListAsync()) :
              //            Problem("Entity set 'ApplicationDbContext.ProductImage'  is null.");
        }

        // GET: AdminProductImage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductImage == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }


        // GET: AdminProductImage/Create
        public IActionResult Create(int? id)
        {
            if (id == null || _context.Product.Count(x => x.Id == id) == 0) { return RedirectToAction("AdminProduct", "Index"); }

            return View(new ProductImage() { ProductId = (int)id});
        }

        // POST: AdminProductImage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductImage productImage)
        {
            if (productImage.ProductId == null || productImage.ProductId == 0) return NotFound();

            if (_context.Product.Any(x => x.Id == productImage.ProductId))
            {
                productImage.ProductName = _context.Product.Where(x => x.Id == productImage.ProductId).FirstOrDefault().Name;
                ModelState.Remove("ProductName");
            }
            ModelState.Remove("FileName");
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var imageFile = HttpContext.Request.Form.Files[0];
                    productImage = await FnHelper.CreateProductImage(imageFile, productImage.ProductId, productImage.IsMainImage);
                }else
                {
                    return NotFound(); //TODO SMT BETTER MAYBE
                }
                

                
                productImage.Id = 0;
                _context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {id = productImage.ProductId});
            }
            return View(productImage);
        }

        // GET: AdminProductImage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductImage == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            return View(productImage);
        }

        // POST: AdminProductImage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Name,IsMainImage,FileName")] ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.Id))
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
            return View(productImage);
        }

        // GET: AdminProductImage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductImage == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: AdminProductImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductImage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductImage'  is null.");
            }
            var productImage = await _context.ProductImage.FindAsync(id);
            if (productImage != null)
            {


                var fileName = "wwwroot" + productImage.FileName.Replace("/", "\\");
                System.IO.File.Delete(fileName);
                _context.ProductImage.Remove(productImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
          return (_context.ProductImage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
