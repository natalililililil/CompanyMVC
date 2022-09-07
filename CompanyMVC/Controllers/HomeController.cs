using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
