using eTrade.Data;
using eTrade.Data.Services.DepartmentService;
using eTrade.Data.Static;
using eTrade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eTrade.Controllers.Backend
{
    [Route("admin/department")]
    [Authorize(Roles = UserRoles.Admin)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentsService _service;
        private readonly AppDbContext _context; private readonly IWebHostEnvironment _webHostEnvironment;

        public DepartmentController(IDepartmentsService service, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
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
            var getDepartment = _context.Departments.Any(n => n.Slug == department.Slug);

            //validate
            if (!ModelState.IsValid)
            {
                return View("../Backend/Department/Create", department);
            }

            //check unique slug
            if (getDepartment)
            {
                ModelState.AddModelError("Slug", "Slug Already Exists");
            }


            //upload image
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(department.ImageFile.FileName);
            string extension = Path.GetExtension(department.ImageFile.FileName);
            string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            department.Image = fullImage;
            string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await department.ImageFile.CopyToAsync(fileStream);
            }

            //add new data
            await _service.AddAsync(department);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _service.GetByIdAsync(id);

            if (department == null) return View("../Backend/Department/NotFound");

            return View("../Backend/Department/Edit", department);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Slug", "ImageFile")] Department department)
        {

            var getDepartment = _context.Departments.AsNoTracking().Where(x => x.Id == department.Id).FirstOrDefault();

            //validate
            if (!ModelState.IsValid)
            {
                return View("../Backend/Department/Edit", department);
            }

            //check unique slug
            if (getDepartment.Slug != department.Slug)
            {
                var data = _context.Departments.Any(n => n.Slug == department.Slug);

                if (data)
                {
                    ModelState.AddModelError("Slug", "Slug Already Exists");
                }
            }


            //upload image

            string imageName = getDepartment.Image;
            if (getDepartment.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(department.ImageFile.FileName);
                string extension = Path.GetExtension(department.ImageFile.FileName);
                string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                department.Image = fullImage;
                string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await department.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                department.Image = imageName;
            }


            //update data by id
            await _service.UpdateAsync(id, department);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _service.GetByIdAsync(id);

            if (department == null) return View("../Backend/Department/NotFound");

            return View("../Backend/Department/Delete", department);
        }


        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _service.GetByIdAsync(id);

            if (department == null) return View("../Backend/Department/NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
