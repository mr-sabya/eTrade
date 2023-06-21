using eTrade.Data.VIewModel;
using eTrade.Models;

namespace eTrade.Data.Services.CategoryService
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllASync();

        Task<CategoryDropdownViewModel> CategoryDropdownsValues();

        Task<Category> GetCategoryByIdAsync(int id);

        Task AddAsync(Category category);

        Task<Category> UpdateAsync(int id, Category category);

        Task DeleteAsync(int id);
    }
}
