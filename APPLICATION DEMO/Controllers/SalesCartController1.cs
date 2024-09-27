using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using APPLICATION_DEMO.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APPLICATION_DEMO.Controllers
{
    public class SalesCartController1 : Controller
    {
        private readonly foodRrpository _foodRrpository;
        private readonly SalesCart _salesCart;

        public SalesCartController1(foodRrpository foodRrpository, SalesCart salesCart)
        {
            _foodRrpository = foodRrpository;
            _salesCart = salesCart;
        }

        public ViewResult Index()
        {
            var serviceProvider = HttpContext.RequestServices;
            var cart = SalesCart.GetCart(serviceProvider);
            _salesCart.AddCartItems = cart.AddCartItems;

            var scVM = new SalesCartViewModel
            {
                SalesCart = _salesCart,
                salescarttotal = cart.GetTotal()
            };

            return View(scVM);
        }

        public RedirectToActionResult addtosalescart(int foodId)
        {
            var selectedFood = _foodRrpository.Foods.FirstOrDefault(f => f.FoodID == foodId);
            if (selectedFood != null)
            {
                _salesCart.AddToCart(selectedFood, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult removetosalescart(int foodId)
        {
            var selectedFood = _foodRrpository.Foods.FirstOrDefault(f => f.FoodID == foodId);
            if (selectedFood != null)
            {
                _salesCart.RemoveFromCart(selectedFood);
            }
            return RedirectToAction("Index");
        }
    }
}
