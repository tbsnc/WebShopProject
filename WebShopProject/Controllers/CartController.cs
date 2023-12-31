﻿using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using WebShopProject.Data;
using WebShopProject.Extensions;

namespace WebShopProject.Controllers
{
    public class CartController : Controller
    {
        public const string SessionKeyName = "_cart";
        private readonly ApplicationDbContext _context;


        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);

            if (cart == null)
            {
                cart = new List<CartItem>();
            }
       
            foreach (CartItem item in cart)
            {
                item.Product.ProductImage = _context.ProductImage != null ? _context.ProductImage.Where(x => x.ProductId == item.Product.Id).ToList() : null;
                item.PricePerProduct = item.GetTotalPrice();
            }
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
            
            if(cart == null) cart = new List<CartItem>();

            if(cart.Count > 0)
            {
                if(cart.Any(x => x.Product.Id == productId))
                {
                    cart.FirstOrDefault(x => x.Product.Id == productId).Quantity++;
                }
                else
                {
                    CartItem item = new CartItem()
                    {
                        Product = _context.Product.Where(x => x.Id == productId).FirstOrDefault(),
                        Quantity = 1,
                    };

                    cart.Add(item);
                }
                HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            }
            else
            {
                CartItem item = new CartItem()
                {
                    Product = _context.Product.Where(x => x.Id == productId).FirstOrDefault(),
                    Quantity = 1,
                };

                cart.Add(item);
                HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            }
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);

            if (cart != null && cart.Count > 0)
            {
                cart.Remove(cart.FirstOrDefault(x => x.Product.Id == productId));
            }
            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeCartItemQuantity(int productId, int quantity)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);

            if (cart != null)
            {
                if (cart.Any(x => x.Product.Id == productId))
                {
                    if (quantity == 0)
                    {
                        cart.Remove(cart.Find(x => x.Product.Id == productId));
                    }
                    else 
                    { 
                        cart.FirstOrDefault(x => x.Product.Id == productId).Quantity = quantity; 
                    }

                    
                }
            }

            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            return RedirectToAction("Index");
        }
    }
}
