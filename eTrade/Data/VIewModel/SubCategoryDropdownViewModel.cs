using eTrade.Models;

namespace eTrade.Data.ViewModel
{   
    public class SubCategoryDropdownViewModel
    {
        public SubCategoryDropdownViewModel() {
            Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
    }
}
