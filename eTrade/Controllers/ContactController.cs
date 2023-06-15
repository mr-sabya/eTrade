using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
