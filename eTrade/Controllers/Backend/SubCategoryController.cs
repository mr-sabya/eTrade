using eTrade.Data;
using eTrade.Data.Services.SubCategoryService;
using eTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Controllers.Backend
{
    [Route("admin/sub-category")]
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
            var data = await _service.GetAllASync();
            return View("../Backend/SubCategory/Index", data);
        }


        [HttpGet("create")]
        public async Task<IActionResult> Create()
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


            return View("../Backend/SubCategory/Create");
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Name", "Slug", "CategoryId")] SubCategory subCategory)
        {
            var data = _context.SubCategories.Any(n => n.Slug == subCategory.Slug);

            if (data)
            {
                ModelState.AddModelError("Slug", "Slug Already Exists");
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

                return View("../Backend/SubCategory/Create", subCategory);
            }
            await _service.AddAsync(subCategory);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);

            if (category == null) { }

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

            return View("../Backend/SubCategory/Edit", category);
        }

    }
}
