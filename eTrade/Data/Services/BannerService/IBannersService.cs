using eTrade.Data.ViewModel;
using eTrade.Models;

namespace eTrade.Data.Services.BannerService
{
    public interface IBannersService
    {
        Task<IEnumerable<Banner>> GetAllASync();

        Task<Banner> GetBannerByIdAsync(int id);

        Task AddAsync(Banner banner);

        Task<Banner> UpdateAsync(int id, Banner banner);

        Task DeleteAsync(int id);
    }
}
