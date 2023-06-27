using eTrade.Data;
using eTrade.Data.Services.TestimonialService;
using eTrade.Data.Static;
using eTrade.Data.ViewModel;
using eTrade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eTrade.Controllers.Backend
{
    [Route("admin/testimonial")]
    [Authorize(Roles = UserRoles.Admin)]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialsService _service;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestimonialController(ITestimonialsService service, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View("../Backend/Testimonial/Index", data);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("../Backend/Testimonial/Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Name", "Company", "Feedback", "ImageFile")] Testimonial testimonial)
        {
            if (!ModelState.IsValid)
            {
                return View("../Backend/Testimonial/Create", testimonial);
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(testimonial.ImageFile.FileName);
            string extension = Path.GetExtension(testimonial.ImageFile.FileName);
            string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            testimonial.Image = fullImage;
            string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await testimonial.ImageFile.CopyToAsync(fileStream);
            }


            await _service.AddAsync(testimonial);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var testimonial = await _service.GetByIdAsync(id);

            if (testimonial == null) { }

            var response = new TestimonialEditViewModel()
            {
                Id = testimonial.Id,
                Name = testimonial.Name,
                Company = testimonial.Company,
                Feedback = testimonial.Feedback,
                Image = testimonial.Feedback,
                
            };

            return View("../Backend/Testimonial/Edit", response);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, TestimonialEditViewModel testimonial)
        {
            //service = await _service.GetByIdAsync(id);
            var getTestimonial = _context.Testimonials.AsNoTracking().Where(x => x.Id == testimonial.Id).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return View("../Backend/Testimonial/Edit", testimonial);
            }

            string imageName = getTestimonial.Image;

            if (testimonial.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(testimonial.ImageFile.FileName);
                string extension = Path.GetExtension(testimonial.ImageFile.FileName);
                string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                testimonial.Image = fullImage;
                string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await testimonial.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                testimonial.Image = imageName;
            }

            await _service.UpdateTestimonialAsync(testimonial);
            return RedirectToAction(nameof(Index));
        }
    }
}
