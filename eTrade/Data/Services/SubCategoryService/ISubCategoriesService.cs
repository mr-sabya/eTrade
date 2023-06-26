using eTrade.Data.Base;
using eTrade.Data.ViewModel;
using eTrade.Models;

namespace eTrade.Data.Services.SubCategoryService
{
    public interface ISubCategoriesService : IEntityBaseRepository<SubCategory>
    {
        Task<SubCategoryDropdownViewModel> SubCategoryDropdownsValues();
    }
}
