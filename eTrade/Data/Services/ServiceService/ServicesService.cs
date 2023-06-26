using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.ServiceService
{
    public class ServicesService : IServicesService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServicesService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddAsync(Service service)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(service.ImageFile.FileName);
            string extension = Path.GetExtension(service.ImageFile.FileName);
            string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            service.Image = fullImage;
            string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await service.ImageFile.CopyToAsync(fileStream);
            }

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Service>> GetAllASync()
        {
            var result = await _context.Services.ToListAsync();
            return result;
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            var result = await _context.Services.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public Task<Service> UpdateAsync(int id, Service service)
        {
            throw new NotImplementedException();
        }
    }
}
