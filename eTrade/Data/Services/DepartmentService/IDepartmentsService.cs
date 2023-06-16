using eTrade.Models;

namespace eTrade.Data.Services.DepartmentService
{
    public interface IDepartmentsService
    {
        Task<IEnumerable<Department>> GetAllASync();

        Task<Department> GetDepartmentByIdAsync(int id);

        Task AddAsync(Department department);

        Task<Department> UpdateAsync(int id, Department department);

        Task DeleteAsync(int id);
    }
}
