using eTrade.Data.Base;
using eTrade.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.BannerService
{
    public class BannersService : EntityBaseRepository<Banner>, IBannersService
    {
        
        public BannersService(AppDbContext context) : base(context)
        {
           
        }


    }
}
