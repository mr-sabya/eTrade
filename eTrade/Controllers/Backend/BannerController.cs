using eTrade.Data;
using eTrade.Data.Services.BannerService;
using eTrade.Data.Static;
using eTrade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eTrade.Controllers.Backend
{
    [Route("admin/banner")]
    [Authorize(Roles = UserRoles.Admin)]
    public class BannerController : Controller
    {
        private readonly IBannersService _service;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannerController(IBannersService service,AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View("../Backend/Banner/Index", data);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View("../Backend/Banner/Create");
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Icon", "Type", "Title", "Link", "ImageFile")] Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return View("../Backend/Banner/Create", banner);
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(banner.ImageFile.FileName);
            string extension = Path.GetExtension(banner.ImageFile.FileName);
            string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            banner.Image = fullImage;
            string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await banner.ImageFile.CopyToAsync(fileStream);
            }

            await _service.AddAsync(banner);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _service.GetByIdAsync(id);

            if (banner == null) View("../Backend/Banner/NotFound");

            return View("../Backend/Banner/Edit", banner);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Icon", "Type", "Title", "Link", "ImageFile")] Banner banner)
        {
            //upload image
            var getBanner = _context.Banners.AsNoTracking().Where(x => x.Id == banner.Id).FirstOrDefault();

            //validation
            if (!ModelState.IsValid)
            {
                return View("../Backend/Banner/Edit", getBanner);
            }

            //upload image
            string imageName = getBanner.Image;

            if (banner.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(banner.ImageFile.FileName);
                string extension = Path.GetExtension(banner.ImageFile.FileName);
                string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                banner.Image = fullImage;
                string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await banner.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                banner.Image = imageName;
            }

            //update data by id
            await _service.UpdateAsync(id, banner);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _service.GetByIdAsync(id);

            if (banner == null) View("../Backend/Banner/NotFound");

            return View("../Backend/Banner/Delete", banner);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _service.GetByIdAsync(id);

            if (banner == null) View("../Backend/Banner/NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
