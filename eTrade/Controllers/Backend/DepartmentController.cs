using eTrade.Data.Services.DepartmentService;
using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers.Backend
{
    [Route("admin/department")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentsService _service;

        public DepartmentController(IDepartmentsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll();
            return View("../Backend/Department/Index", data);
        }
    }
}
