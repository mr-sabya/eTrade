using eTrade.Models;

namespace eTrade.Data.VIewModel
{
    public class HomePage
    {
        public HomePage()
        {
            Departments = new List<Department>();
        }

        public List<Department> Departments { get; set; }
        
    }
}
