using eTrade.Data;
using eTrade.Data.Services.SubCategoryService;
using eTrade.Data.Static;
using eTrade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eTrade.Controllers.Backend
{
    [Route("admin/sub-category")]
    [Authorize(Roles = UserRoles.Admin)]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoriesService _service;
        private readonly AppDbContext _context;

        public SubCategoryController(ISubCategoriesService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Category);
            return View("../Backend/SubCategory/Index", data);
        }


        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            //sub category drop down items
            var subCategoryDropDownData = await _service.SubCategoryDropdownsValues();
            List<SelectListItem> categories = subCategoryDropDownData.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Department.Name + " - " + c.Name,
            }).ToList();

            var categoryTip = new SelectListItem()
            {
                Value = null,
                Text = "--- select category ---"
            };
            categories.Insert(0, categoryTip);

            ViewBag.CategoryId = new SelectList(categories, "Value", "Text");


            return View("../Backend/SubCategory/Create");
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Name", "Slug", "CategoryId")] SubCategory subCategory)
        {
            var getSubCategory = _context.SubCategories.Any(n => n.Slug == subCategory.Slug);

            //validate
            if (!ModelState.IsValid)
            {
                //sub category drop down items
                var subCategoryDropDownData = await _service.SubCategoryDropdownsValues();
                List<SelectListItem> categories = subCategoryDropDownData.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Department.Name + " - " + c.Name,
                }).ToList();

                var categoryTip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select category ---"
                };
                categories.Insert(0, categoryTip);

                ViewBag.CategoryId = new SelectList(categories, "Value", "Text");

                return View("../Backend/SubCategory/Create", subCategory);
            }


            //check unique slug
            if (getSubCategory)
            {
                ModelState.AddModelError("Slug", "Slug Already Exists");
            }

            //add new data
            await _service.AddAsync(subCategory);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var subCategory = await _service.GetByIdAsync(id);

            if (subCategory == null) return View("../Backend/SubCategory/NotFound");


            //sub category drop down items
            var subCategoryDropDownData = await _service.SubCategoryDropdownsValues();
            List<SelectListItem> categories = subCategoryDropDownData.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Department.Name + " - " + c.Name,
            }).ToList();

            var categoryTip = new SelectListItem()
            {
                Value = null,
                Text = "--- select category ---"
            };
            categories.Insert(0, categoryTip);

            ViewBag.CategoryId = new SelectList(categories, "Value", "Text");

            return View("../Backend/SubCategory/Edit", subCategory);
        }



        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Slug", "CategoryId")] SubCategory subCategory)
        {

            var getSubCategory = _context.SubCategories.AsNoTracking().Where(x => x.Id == subCategory.Id).FirstOrDefault();

            if (getSubCategory.Slug != subCategory.Slug)
            {
                var data = _context.SubCategories.Any(n => n.Slug == subCategory.Slug);

                if (data)
                {
                    ModelState.AddModelError("Slug", "Slug Already Exists");
                }
            }

            if (!ModelState.IsValid)
            {
                var subCategoryDropDownData = await _service.SubCategoryDropdownsValues();
                List<SelectListItem> categories = subCategoryDropDownData.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Department.Name + " - " + c.Name,
                }).ToList();

                var categoryTip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select category ---"
                };
                categories.Insert(0, categoryTip);

                ViewBag.CategoryId = new SelectList(categories, "Value", "Text");

                return View("../Backend/SubCategory/Edit", subCategory);
            }

            await _service.UpdateAsync(id, subCategory);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subCategory = await _service.GetByIdAsync(id);

            if (subCategory == null) return View("../Backend/SubCategory/NotFound");

            return View("../Backend/SubCategory/Delete", subCategory);
        }



        [HttpPost("delete/{id}"), ActionName("delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _service.GetByIdAsync(id);

            if (subCategory == null) return View("../Backend/SubCategory/NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
