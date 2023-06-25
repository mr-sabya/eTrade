using eTrade.Data.ViewModel;

namespace eTrade.Data.Services.HomeService
{
    public interface IHomeService
    {
        Task<HomePageViewModel> GetHomePageItems();
    }
}
