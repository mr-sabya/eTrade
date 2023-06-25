using eTrade.Data.ViewModel;
using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.CategoryService
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryDropdownViewModel> CategoryDropdownsValues()
        {
            var response = new CategoryDropdownViewModel();
            response.Departments = await _context.Departments.OrderBy(x => x.Name).ToListAsync();

            return response;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(n => n.Id == id);
            _context.Categories.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllASync()
        {
            var result = await _context.Categories.Include( n => n.Department).ToListAsync();
            return result;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
