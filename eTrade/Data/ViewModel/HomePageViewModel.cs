using eTrade.Models;

namespace eTrade.Data.ViewModel
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            Departments = new List<Department>();
            Banners = new List<Banner>();
            Services = new List<Service>();
        }

        public List<Department> Departments { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Service> Services { get; set; }
    }
}
