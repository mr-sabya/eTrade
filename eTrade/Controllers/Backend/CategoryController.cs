using eTrade.Data;
using eTrade.Data.Services.CategoryService;
using eTrade.Data.Static;
using eTrade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eTrade.Controllers.Backend
{
    [Route("admin/category")]
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoriesService _service;
        private readonly AppDbContext _context;

        public CategoryController(ICategoriesService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Department);
            return View("../Backend/Category/Index", data);
        }


        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var categoryDropDownData = await _service.CategoryDropdownsValues();
            ViewBag.DepartmentId = new SelectList(categoryDropDownData.Departments, "Id", "Name");
            return View("../Backend/Category/Create");
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Name", "Slug", "DepartmentId")] Category category)
        {
            var getCategory = _context.Categories.Any(n => n.Slug == category.Slug);

            //validation
            if (!ModelState.IsValid)
            {
                var categoryDropDownData = await _service.CategoryDropdownsValues();
                ViewBag.DepartmentId = new SelectList(categoryDropDownData.Departments, "Id", "Name");

                return View("../Backend/Category/Create", category);
            }

            //check unique slug
            if (getCategory)
            {
                ModelState.AddModelError("Slug", "Slug Already Exists");
            }

            //add new data
            await _service.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null) return View("../Backend/Category/NotFound");

            var categoryDropDownData = await _service.CategoryDropdownsValues();
            ViewBag.DepartmentId = new SelectList(categoryDropDownData.Departments, "Id", "Name");

            return View("../Backend/Category/Edit", category);
        }



        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Slug", "DepartmentId")] Category category)
        {

            var getCategory = _context.Categories.AsNoTracking().Where(x => x.Id == category.Id).FirstOrDefault();

            //validation
            if (!ModelState.IsValid)
            {
                var categoryDropDownData = await _service.CategoryDropdownsValues();
                ViewBag.DepartmentId = new SelectList(categoryDropDownData.Departments, "Id", "Name");
                return View("../Backend/Category/Edit", category);
            }

            //check unique slug
            if (getCategory.Slug != category.Slug)
            {
                var data = _context.Categories.Any(n => n.Slug == category.Slug);

                if (data)
                {
                    ModelState.AddModelError("Slug", "Slug Already Exists");
                }
            }

            
            //update data by id
            await _service.UpdateAsync(id, category);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null) return View("../Backend/Category/NotFound");

            return View("../Backend/Category/Delete", category);
        }



        [HttpPost("delete/{id}"), ActionName("delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null) return View("../Backend/Category/NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
