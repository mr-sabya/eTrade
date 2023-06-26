using eTrade.Models;

namespace eTrade.Data.Services.ServiceService
{
    public interface IServicesService
    {
        Task<IEnumerable<Service>> GetAllASync();

        Task<Service> GetServiceByIdAsync(int id);

        Task AddAsync(Service service);

        Task<Service> UpdateAsync(int id, Service service);

        Task DeleteAsync(int id);
    }
}
