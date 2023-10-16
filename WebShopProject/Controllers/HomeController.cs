﻿using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShopProject.Data;
using WebShopProject.Models;

namespace WebShopProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            if(_context.Product != null)
            {
                products = _context.Product.ToList();
            }
            
            

            foreach(Product product in products)
            {
                product.ProductImage = _context.ProductImage != null ? _context.ProductImage.Where(x => x.ProductId == product.Id).ToList() : null;
                product.ProductCategory = _context.ProductCategory != null ? _context.ProductCategory.Where(x => x.ProductId == product.Id).ToList() : null;
                if (product.Description != null && product.Description.Length > 75)
                {
                

                    product.Description = product.Description.Truncate(75);
                }
            }

            ViewBag.Category = _context.Category != null ? _context.Category.ToList() : null;

            return View(products.Count > 0 ? products : new Product());
        }
        public IActionResult FilterByCategory(int id)
        {
            if(_context.Product == null) RedirectToAction("Index");

            List<Product> productsFiltered = new List<Product>();
            
            if (_context.Category != null)
            {
                foreach(Product product in _context.Product.Where(x => x.Active == true)) 
                {
                    if (_context.ProductCategory.Any(x => x.ProductId == product.Id && x.CategoryId == id))
                    {
                        product.ProductImage = _context.ProductImage.Where(x => x.ProductId == product.Id).ToList();
                        product.ProductCategory = _context.ProductCategory.Where(x => x.ProductId == product.Id).ToList();
                        productsFiltered.Add(product);
                    }
                }
            }
            
            ViewBag.Category = _context.Category != null ? _context.Category.ToList() : null;
            return View("Index", productsFiltered);
        }

        public IActionResult ProductDetails(int? id)
        {
            if(id  == null) RedirectToAction("Index");

            Product product = _context.Product != null ? _context.Product.Where(x => x.Id == id).FirstOrDefault() : null;

            if (product != null)
            {
                product.ProductImage = _context.ProductImage.Where(x => x.ProductId == id).ToList();
            }

            return View(product);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}