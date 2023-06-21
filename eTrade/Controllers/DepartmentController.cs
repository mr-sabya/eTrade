using eTrade.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;


        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Details(string slug)
        {
            var department = _context.Departments.FirstOrDefaultAsync(n => n.Slug == slug);
            return View(department);
        }
    }
}
