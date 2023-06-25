using eTrade.Data.ViewModel;
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


        public async Task<HomePageViewModel> GetHomePageItems()
        {
            var response = new HomePageViewModel()
            {
                Departments = await _context.Departments.Include(n => n.categories)
                .ThenInclude(n => n.subCategories).OrderBy(n => n.Name).ToListAsync(),

                Banners = await _context.Banners.ToListAsync(),
                
            };

            return response;
        }
    }
}
