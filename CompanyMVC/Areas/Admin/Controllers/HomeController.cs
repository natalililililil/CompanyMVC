using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {   
        [Authorize(Roles = "admin")]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
