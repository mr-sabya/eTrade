using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.DepartmentService
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DepartmentsService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddAsync(Department department)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(department.ImageFile.FileName);
            string extension = Path.GetExtension(department.ImageFile.FileName);
            string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            department.Image = fullImage;
            string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await department.ImageFile.CopyToAsync(fileStream);
            }

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(n => n.Id == id);
            _context.Departments.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllASync()
        {
            var result = await _context.Departments.ToListAsync();
            return result;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<Department> UpdateAsync(int id, Department department)
        {
            var data  = _context.Departments.AsNoTracking().Where(x => x.Id == department.Id).FirstOrDefault();
            string imageName = data.Image;

            if (department.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(department.ImageFile.FileName);
                string extension = Path.GetExtension(department.ImageFile.FileName);
                string fullImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                department.Image = fullImage;
                string path = Path.Combine(wwwRootPath + "/Image/", fullImage);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await department.ImageFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                department.Image = imageName;
            }


            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return department;
        }

        
    }
}
