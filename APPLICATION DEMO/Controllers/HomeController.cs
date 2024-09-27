using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using APPLICATION_DEMO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;

namespace APPLICATION_DEMO.Controllers
{
    public class HomeController : Controller
    {
        private readonly foodRrpository _foodRrpository;

        public HomeController(foodRrpository foodRrpository)
        {
            _foodRrpository = foodRrpository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                // Populate this with actual image URLs
                CarouselImages = new List<string>
                {
                    "~/Images/f2app.jpeg",
                    "~/Images/carousel2.jpg",
                    "~/Images/f3.jpeg"
                },
                // Convert PreferredFood to List<Food>
                PreferredFood = _foodRrpository.PreferredFood?.ToList() ?? new List<Food>()
            };

            return View(homeViewModel);
        }

        // Uncomment these methods if you need them later
        // public IActionResult Privacy()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}
