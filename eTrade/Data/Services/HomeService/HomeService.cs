using eTrade.Data.VIewModel;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.HomeService
{
    public class HomeService : IHomeService
    {
        private readonly AppDbContext _context;

        public HomeService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<HomePage> GetHomePageItems()
        {
            var response = new HomePage()
            {
                Departments = await _context.Departments.OrderBy(n => n.Name).ToListAsync()
            };

            return response;
        }
    }
}
