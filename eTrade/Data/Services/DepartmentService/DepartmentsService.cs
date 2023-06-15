using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.DepartmentService
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly AppDbContext _context;

        public DepartmentsService(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Department department)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            var result = await _context.Departments.ToListAsync();
            return result;
        }

        public Department GetDepartmentById(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Department Update(int id, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
