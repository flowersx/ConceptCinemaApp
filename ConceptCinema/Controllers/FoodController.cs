using Microsoft.AspNetCore.Mvc;

namespace YourAppNamespace.Controllers
{
    public class FoodController : Controller
    {
        // Acțiunea Index care va fi apelată când utilizatorul accesează /Food
        public IActionResult Index()
        {
            return View();
        }
    }
}
