using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers.Backend
{
    [Route("admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("../Backend/Home/Index");
        }
    }
}
