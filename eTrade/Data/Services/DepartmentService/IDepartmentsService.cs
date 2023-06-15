using eTrade.Models;

namespace eTrade.Data.Services.DepartmentService
{
    public interface IDepartmentsService
    {
        Task<IEnumerable<Department>> GetAll();

        Department GetDepartmentById(int departmentId);

        void Add(Department department);

        Department Update(int id, Department department);

        void Delete(int id);
    }
}
