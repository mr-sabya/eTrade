using eTrade.Data;
using eTrade.Data.Services.DepartmentService;
using Microsoft.EntityFrameworkCore;

namespace eTrade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //DBContext
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder
                .Configuration.GetConnectionString("DefaultConnectionString")));

            //service configuration
            builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}