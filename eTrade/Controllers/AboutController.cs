using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
