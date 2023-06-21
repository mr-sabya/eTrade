using eTrade.Data.VIewModel;
using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.SubCategoryService
{
    public class SubCategoryService : ISubCategoriesService
    {
        private readonly AppDbContext _context;

        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SubCategory subCategory)
        {
            await _context.SubCategories.AddAsync(subCategory);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SubCategory>> GetAllASync()
        {
            var result = await _context.SubCategories.Include(n => n.Category).ToListAsync();
            return result;
        }

        public async Task<SubCategory> GetCategoryByIdAsync(int id)
        {
            var result = await _context.SubCategories.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<SubCategoryDropdownViewModel> SubCategoryDropdownsValues()
        {
            var response = new SubCategoryDropdownViewModel();
            response.Categories = await _context.Categories.OrderBy(x => x.Name).Include(n => n.Department).ToListAsync();

            return response;
        }

        public Task<Category> UpdateAsync(int id, SubCategory subCategory)
        {
            throw new NotImplementedException();
        }
    }
}
