using eTrade.Data.Base;
using eTrade.Data.ViewModel;
using eTrade.Models;

namespace eTrade.Data.Services.CategoryService
{
    public interface ICategoriesService : IEntityBaseRepository<Category>
    {
        Task<CategoryDropdownViewModel> CategoryDropdownsValues();
    }
}
