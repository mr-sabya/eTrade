using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
