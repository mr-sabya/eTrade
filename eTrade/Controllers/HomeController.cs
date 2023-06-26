using eTrade.Data;
using eTrade.Data.Services.DepartmentService;
using eTrade.Data.Services.HomeService;
using eTrade.Data.ViewModel;
using eTrade.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

namespace eTrade.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IHomeService _service;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IHomeService service)
        {
            _logger = logger;
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var homePageItems = await _service.GetHomePageItems();

            ViewBag.Departments = homePageItems.Departments;
            ViewBag.Banners = homePageItems.Banners;
            ViewBag.Services = homePageItems.Services;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}