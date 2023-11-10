using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShopProject.Data;
using WebShopProject.Extensions;
using WebShopProject.Models;


namespace WebShopProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private const string SessionKeyName = "_cart";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FnHelper _fnHelper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _fnHelper = new FnHelper(context);
        }

        public IActionResult Index(string? message)
        {
            if (message != null) { ViewBag.Message = message; }

            List<Product> products = new List<Product>();
            if (_context.Product != null)
            {
                products = _context.Product.ToList();
            } 



            foreach (Product product in products)
            { 
                if (_context.ProductImage.Any(x => x.ProductId == product.Id))
                {
                    product.ProductImage = _context.ProductImage.Where(x => x.ProductId == product.Id).ToList();
                }else
                {
                    product.ProductImage = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            FileName = "/images/default.png",
                            ProductId = product.Id,
                            ProductName = product.Name,
                            IsMainImage = true,
                            Name = "Default"
                        }
                    };
                }


                product.ProductCategory = _context.ProductCategory != null ? _context.ProductCategory.Where(x => x.ProductId == product.Id).ToList() : null;
                if (product.Description != null && product.Description.Length > 75)
                {


                    product.Description = product.Description.Truncate(75);
                }
            }

            ViewBag.Category = _context.Category != null ? _context.Category.ToList() : null;
             products = _fnHelper.ShuffleProductList(products);
            return View(products.Count > 0 ? products : new Product());
        }
        public IActionResult FilterByCategory(int id)
        {
            if (_context.Product == null) RedirectToAction("Index");

            List<Product> productsFiltered = new List<Product>();

            if (_context.Category != null)
            {
                foreach (Product product in _context.Product.Where(x => x.Active == true))
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
            if (id == null) RedirectToAction("Index");

            Product product = _context.Product != null ? _context.Product.Where(x => x.Id == id).FirstOrDefault() : null;

            if (product != null)
            {
                product.ProductImage = _context.ProductImage.Where(x => x.ProductId == id).ToList();
            }

            return View(product);
        }
        [Authorize]
        public IActionResult Order(List<string> error)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);

            if (cart == null || cart.Count == 0) { return RedirectToAction("Index", new { message = "Cart is empty!" }); }

            error = _fnHelper.VerifyCartProduct(cart);

     

            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            if (cart == null || cart.Count == 0) { return RedirectToAction("Index", new { message = "Cart is empty!" }); }

            foreach (var item in cart)
            {
                item.Product.ProductImage = _context.ProductImage.Where(x => x.ProductId == item.Product.Id).ToList();
                item.Product.ProductCategory = _context.ProductCategory.Where(x => x.ProductId == item.Product.Id).ToList();
            }

            ViewBag.Error = error;

            ViewBag.SelectCountry = _context.Country.ToList();

            //auto fill billing info
            var user = _context.Users.Find(_userManager.GetUserId(User));
            if (user != null)
            {
                ViewBag.User = user;
            }
            return View(cart);
        }


        [HttpPost]
        public IActionResult CreateOrder(Order order, string shipSameAsBill)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);

            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Index", new { message = "Cart is empty!" });
            }

            var Error = new List<string>();

            Error = _fnHelper.VerifyCartProduct(cart);



            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            if (Error.Count > 0)
            {
                return RedirectToAction("Order", new { error = Error });
            }

            if (shipSameAsBill == "on")
            {
                order.ShippingAddress = order.BillingAddress;
                order.ShippingCity = order.BillingCity;
                order.ShippingEmail = order.BillingEmail;
                order.ShippingCountry = order.BillingCountry;
                order.ShippingFirstName = order.BillingFirstName;
                order.ShippingLastName = order.BillingLastName;
                order.ShippingPostalCode = order.BillingPostalCode;
                order.ShippingPhoneNumber = order.BillingPhoneNumber;
            }

            order.DateCreated = DateTime.Now;
            order.Total = cart.Sum(x => x.GetTotalPrice());
            order.UserId = _userManager.GetUserId(User);
            ModelState.Remove("OrderItems");
            if (order.Message == null)
            {
                ModelState.Remove("Message");
                order.Message = string.Empty;
            }
                

            if (ModelState.IsValid)
            {
                _context.Order.Add(order);
                _context.SaveChanges();

                int orderId = order.Id;

                foreach (var item in cart)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = orderId;
                    orderItem.ProductId = item.Product.Id;
                    orderItem.Price = item.Product.Price;
                    orderItem.Quantity = item.Quantity;

                    _context.OrderItem.Add(orderItem);
                    _context.Product.Find(item.Product.Id).Quantity -= item.Quantity;
                    _context.SaveChanges();
                }

                HttpContext.Session.SetObjectAsJson(SessionKeyName, "");
                return RedirectToAction("Index", new { message = "Order completed! Thank you for shopping" });
            }
            else
            {
                Error.Add("Order is not valid");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        Error.Add(modelError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Order", new { error = Error });
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