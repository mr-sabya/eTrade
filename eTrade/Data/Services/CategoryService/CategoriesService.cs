using eTrade.Data.Base;
using eTrade.Data.ViewModel;
using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.CategoryService
{
    public class CategoriesService : EntityBaseRepository<Category>, ICategoriesService
    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //get category form drop down data
        public async Task<CategoryDropdownViewModel> CategoryDropdownsValues()
        {
            var response = new CategoryDropdownViewModel();
            response.Departments = await _context.Departments.OrderBy(x => x.Name).ToListAsync();

            return response;
        }
 
    }
}
