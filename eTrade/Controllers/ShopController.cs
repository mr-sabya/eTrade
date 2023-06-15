using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
