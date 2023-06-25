using eTrade.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.BannerService
{
    public class BannersService : IBannersService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannersService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task AddAsync(Banner banner)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(banner.ImageFile.FileName);
            string extension = Path.GetExtension(banner.ImageFile.FileName);
            string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            banner.Image = fullImage;
            string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await banner.ImageFile.CopyToAsync(fileStream);
            }

            await _context.Banners.AddAsync(banner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Banners.FirstOrDefaultAsync(n => n.Id == id);
            _context.Banners.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Banner>> GetAllASync()
        {
            var result = await _context.Banners.ToListAsync();
            return result;
        }

        public async Task<Banner> GetBannerByIdAsync(int id)
        {
            var result = await _context.Banners.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<Banner> UpdateAsync(int id, Banner banner)
        {
            var data = _context.Banners.AsNoTracking().Where(x => x.Id == banner.Id).FirstOrDefault();
            string imageName = data.Image;

            if (banner.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(banner.ImageFile.FileName);
                string extension = Path.GetExtension(banner.ImageFile.FileName);
                string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                banner.Image = fullImage;
                string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await banner.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                banner.Image = imageName;
            }


            _context.Entry(banner).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return banner;
        }
    }
}
