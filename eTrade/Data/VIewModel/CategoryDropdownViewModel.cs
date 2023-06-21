using eTrade.Models;

namespace eTrade.Data.VIewModel
{
    public class CategoryDropdownViewModel
    {
        public CategoryDropdownViewModel() 
        {
            Departments = new List<Department>();
        }

        public List<Department> Departments { get; set; }
    }
}
