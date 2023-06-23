using eTrade.Data.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers.Backend
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("../Backend/Home/Index");
        }
    }
}
