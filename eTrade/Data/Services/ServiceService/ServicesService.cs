using eTrade.Data.Base;
using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.ServiceService
{
    public class ServicesService : EntityBaseRepository<Service>, IServicesService
    {
        public ServicesService(AppDbContext context) : base(context)
        {
        }
    }
}
