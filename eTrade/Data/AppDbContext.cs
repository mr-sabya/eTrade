using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
    }
}
