using eTrade.Data.VIewModel;

namespace eTrade.Data.Services.HomeService
{
    public interface IHomeService
    {
        Task<HomePage> GetHomePageItems();
    }
}
