using eTrade.Data.ViewModel;
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

        public async Task DeleteAsync(int id)
        {
            var result = await _context.SubCategories.FirstOrDefaultAsync(n => n.Id == id);
            _context.SubCategories.Remove(result);
            await _context.SaveChangesAsync();
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

        public async Task<SubCategory> UpdateAsync(int id, SubCategory subCategory)
        {
            _context.Entry(subCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return subCategory;
        }
    }
}
