using eTrade.Data.ViewModel;
using eTrade.Models;

namespace eTrade.Data.Services.SubCategoryService
{
    public interface ISubCategoriesService
    {
        Task<IEnumerable<SubCategory>> GetAllASync();

        Task<SubCategoryDropdownViewModel> SubCategoryDropdownsValues();

        Task<SubCategory> GetCategoryByIdAsync(int id);

        Task AddAsync(SubCategory subCategory);

        Task<Category> UpdateAsync(int id, SubCategory subCategory);

        Task DeleteAsync(int id);
    }
}
