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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServiceController(IServicesService service, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
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

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(service.ImageFile.FileName);
            string extension = Path.GetExtension(service.ImageFile.FileName);
            string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            service.Image = fullImage;
            string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await service.ImageFile.CopyToAsync(fileStream);
            }


            await _service.AddAsync(service);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetByIdAsync(id);

            if (service == null) { }

            return View("../Backend/Service/Edit", service);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Text", "ImageFile")] Service service)
        {
            //service = await _service.GetByIdAsync(id);
            var getService = _context.Services.AsNoTracking().Where(x => x.Id == service.Id).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return View("../Backend/Service/Edit", getService);
            }
         
            string imageName = getService.Image;

            if (service.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(service.ImageFile.FileName);
                string extension = Path.GetExtension(service.ImageFile.FileName);
                string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                service.Image = fullImage;
                string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await service.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                service.Image = imageName;
            }

            await _service.UpdateAsync(id, service);
            return RedirectToAction(nameof(Index));
        }
    }
}
