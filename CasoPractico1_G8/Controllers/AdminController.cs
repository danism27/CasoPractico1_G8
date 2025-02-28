using Microsoft.AspNetCore.Mvc;

namespace CasoPractico1_G8.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
