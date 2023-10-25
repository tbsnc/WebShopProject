using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopProject.Data;
using WebShopProject.Models;

namespace WebShopProject.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FnHelper _fnHelper;
        public AdminOrderItemController(ApplicationDbContext context)
        {
            _context= context;
            _fnHelper = new FnHelper(context);
        }


        public IActionResult Index(int? orderId)
        {
            if (orderId == null) return RedirectToAction("Index", "AdminOrder");



            var orderItems = _fnHelper.GetOrderItems(orderId); 
            
            return View(orderItems == null ? new List<OrderItem>() : orderItems);
        }


        public IActionResult Edit(int? id)
        {
            var ordItem = _context.OrderItem.Find(id);
            ordItem.ProductName = _context.Product.Where(x => x.Id == ordItem.ProductId).FirstOrDefault().Name;
            return View(ordItem);
        }

        [HttpPost]
        public IActionResult Edit(OrderItem orderItem)
        {
            if (orderItem.Price <= 0) ModelState.AddModelError("Price", "Price must be greater than 0");
            if(orderItem.Quantity <= 0) ModelState.AddModelError("Quantity", "Quantity must be greater than 0");

            if (ModelState.IsValid)
            {
                var product = _context.Product.Where(x => x.Id == orderItem.ProductId).FirstOrDefault();
                var oldItem = _context.OrderItem.Where(x => x.Id == orderItem.Id).FirstOrDefault();
                var diffQuantity = orderItem.Quantity - oldItem.Quantity;
                var diffPrice = orderItem.Price - oldItem.Price;
                


                if (diffQuantity == 0 && diffPrice == 0) return RedirectToAction("Edit", new { id = orderItem.Id });

                if (diffQuantity < 0)
                {
                    product.Quantity += Math.Abs(diffQuantity);
                }
                else
                {
                    if (product.Quantity > diffQuantity)
                    {
                        product.Quantity -= Math.Abs(diffQuantity);
                    }
                    else
                    {
                        ModelState.AddModelError("Quantity", $"Only {product.Quantity} items are availiable for sale.");
                        //redirect with error product amount not availiable or set to max availiable amount
                    }
                }

                if(diffPrice != 0 || diffQuantity != 0)
                {
                    _context.Order.Find(orderItem.OrderId).Total += (orderItem.Price * orderItem.Quantity) - (oldItem.Price * oldItem.Quantity);
                }
                oldItem.Quantity = orderItem.Quantity; 
                oldItem.Price = orderItem.Price;

                _context.SaveChanges();

                
            }
            
            return View(orderItem);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            
            var orderItem = _context.OrderItem.Find(id);
            orderItem.ProductName = _context.Product.Find(orderItem.ProductId).Name;
            return View(orderItem);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            if(id == null) return RedirectToAction("Index", "AdminOrder");

            var orderItem = _context.OrderItem.Find(id);
            var orderId = orderItem.OrderId;
            
            _context.Product.Find(orderItem.ProductId).Quantity += orderItem.Quantity;
            _context.Order.Find(orderItem.OrderId).Total -= (orderItem.Price * orderItem.Quantity);
            _context.OrderItem.Remove(orderItem);
            _context.SaveChanges();

            return RedirectToAction("Edit", "AdminOrder",new { id = orderId });

        }
    }
}
