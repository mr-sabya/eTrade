using eTrade.Data;
using eTrade.Data.Services.ServiceService;
using eTrade.Data.Static;
using eTrade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eTrade.Controllers.Backend
{
    [Route("admin/service")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ServiceController : Controller
    {
        private readonly IServicesService _service;
        private readonly AppDbContext _context;

        public ServiceController(IServicesService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllASync();
            return View("../Backend/Service/Index", data);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("../Backend/Service/Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Text", "ImageFile")] Service service)
        {
            if (!ModelState.IsValid)
            {
                return View("../Backend/Service/Create", service);
            }
            await _service.AddAsync(service);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetServiceByIdAsync(id);

            if (service == null) { }

            return View("../Backend/Service/Edit", service);
        }
    }
}
