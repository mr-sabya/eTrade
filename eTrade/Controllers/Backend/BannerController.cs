using eTrade.Data.Services.BannerService;
using eTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Controllers.Backend
{
    [Route("admin/banner")]
    public class BannerController : Controller
    {
        private readonly IBannersService _service;

        public BannerController(IBannersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllASync();
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
            await _service.AddAsync(banner);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _service.GetBannerByIdAsync(id);

            if (banner == null) { }

            return View("../Backend/Banner/Edit", banner);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Icon", "Type", "Title", "Link", "ImageFile")] Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return View("../Backend/Banner/Edit", banner);
            }

            await _service.UpdateAsync(id, banner);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _service.GetBannerByIdAsync(id);

            if (banner == null) { }

            return View("../Backend/Banner/Delete", banner);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _service.GetBannerByIdAsync(id);

            if (banner == null) { }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
