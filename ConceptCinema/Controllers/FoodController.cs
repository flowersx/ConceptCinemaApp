using Microsoft.AspNetCore.Mvc;

namespace YourAppNamespace.Controllers
{
    public class FoodController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
