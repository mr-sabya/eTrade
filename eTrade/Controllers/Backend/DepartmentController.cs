using eTrade.Data;
using eTrade.Data.Services.DepartmentService;
using eTrade.Data.Static;
using eTrade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eTrade.Controllers.Backend
{
    [Route("admin/department")]
    [Authorize(Roles = UserRoles.Admin)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentsService _service;
        private readonly AppDbContext _context;

        public DepartmentController(IDepartmentsService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllASync();
            return View("../Backend/Department/Index", data);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("../Backend/Department/Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Name", "Slug", "ImageFile")] Department department)
        {
            var data = _context.Departments.Any(n => n.Slug == department.Slug);

            if(data)
            {
                ModelState.AddModelError("Slug", "Slug Already Exists");
            }

            if (!ModelState.IsValid)
            {
                return View("../Backend/Department/Create", department);
            }
            await _service.AddAsync(department);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _service.GetDepartmentByIdAsync(id);

            if(department == null) { }

            return View("../Backend/Department/Edit", department);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Slug", "ImageFile")] Department department)
        {

            var check = _context.Departments.AsNoTracking().Where(x => x.Id == department.Id).FirstOrDefault();

            if(check.Slug != department.Slug)
            {
                var data = _context.Departments.Any(n => n.Slug == department.Slug);

                if (data)
                {
                    ModelState.AddModelError("Slug", "Slug Already Exists");
                }
            }

            if (!ModelState.IsValid)
            {
                return View("../Backend/Department/Edit", department);
            }

            await _service.UpdateAsync(id, department);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _service.GetDepartmentByIdAsync(id);

            if (department == null) { }

            return View("../Backend/Department/Delete", department);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _service.GetDepartmentByIdAsync(id);

            if (department == null) { }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
