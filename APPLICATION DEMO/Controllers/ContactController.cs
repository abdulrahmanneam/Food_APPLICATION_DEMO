using Microsoft.AspNetCore.Mvc;

namespace APPLICATION_DEMO.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
