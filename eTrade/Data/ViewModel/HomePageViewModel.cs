using eTrade.Models;

namespace eTrade.Data.ViewModel
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            Departments = new List<Department>();
            Banners = new List<Banner>();
        }

        public List<Department> Departments { get; set; }
        public List<Banner> Banners { get; set; }   
        
    }
}
