using eTrade.Data.Base;
using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.DepartmentService
{
    public class DepartmentsService :EntityBaseRepository<Department>, IDepartmentsService
    {
        public DepartmentsService(AppDbContext context) : base(context) { }
    }
}
