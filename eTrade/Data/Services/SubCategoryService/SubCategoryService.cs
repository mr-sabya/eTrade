using eTrade.Data.Base;
using eTrade.Data.ViewModel;
using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.SubCategoryService
{
    public class SubCategoryService : EntityBaseRepository<SubCategory>, ISubCategoriesService
    {
        private readonly AppDbContext _context;

        public SubCategoryService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        
        public async Task<SubCategoryDropdownViewModel> SubCategoryDropdownsValues()
        {
            var response = new SubCategoryDropdownViewModel();
            response.Categories = await _context.Categories.OrderBy(x => x.Name).Include(n => n.Department).ToListAsync();

            return response;
        }

    }
}
