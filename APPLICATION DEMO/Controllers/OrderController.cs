
using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using APPLICATION_DEMO.DAL.Repositories;
using APPLICATION_DEMO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APPLICATION_DEMO.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        FoodDBContext _FoodDB;
        private readonly SalesCart _salesCart;
        public OrderController(IOrderRepository orderRepository, SalesCart salesCart, FoodDBContext FoodDB)
        {
            _orderRepository = orderRepository;
            _salesCart = salesCart;
            _FoodDB = FoodDB;


        }
       
        public IActionResult checkOutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(OrderVeiwModel model)
        {
            var items = _salesCart.getSalesCartItems();
            _salesCart.AddCartItems = items;
            if (_salesCart.AddCartItems.Count == 0)
            {
                ModelState.AddModelError("", "your card Is Empty , Add Some Foods First");
            }
            if (ModelState.IsValid)
            {
                //_orderRepository.CreateOrder(order);


                // _context هو كائن DbContext

                _salesCart.ClearCart();
                return RedirectToAction("checkOutComplete");
            }

            Order order = new Order();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.AddressLine1 = model.AddressLine1;
            order.AddressLine2 = model.AddressLine2;
            order.ZipCode = model.ZipCode;
            order.State = model.State;
            order.Country = model.Country;
            order.PhoneNumber = model.PhoneNumber;
            order.Email = model.Email;
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = order.OrderTotal + 1;
            var addCartItems = _salesCart.AddCartItems;

            _FoodDB.orders.Add(order);
           
            foreach (var item in addCartItems)
            {
                var OrderD = new OrderDetail()
                {
                    Amount = item.amount,
                    FoodId = item.food.FoodID,
                    OrderId = order.OrderId,
                    Price = item.food.Price
                };
                _FoodDB.ordersDetail.Add(OrderD);
            }

            _FoodDB.SaveChanges();
            return View(order);
        }
    }
}
