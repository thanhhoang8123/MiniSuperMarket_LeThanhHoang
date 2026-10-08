using Microsoft.AspNetCore.Mvc;

namespace MiniSupermarket.API.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
