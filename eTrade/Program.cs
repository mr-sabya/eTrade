using eTrade.Data;
using eTrade.Data.Services.CategoryService;
using eTrade.Data.Services.DepartmentService;
using eTrade.Data.Services.HomeService;
using Microsoft.AspNetCore.Mvc;
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

            //Removes the required attribute for non-nullable reference types
            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            //DBContext
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder
                .Configuration.GetConnectionString("DefaultConnectionString")));

            //service configuration
            //admin site
            builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();

            //user site
            builder.Services.AddScoped<IHomeService, HomeService>();

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