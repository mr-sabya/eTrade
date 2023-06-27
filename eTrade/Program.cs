using eTrade.Data;
using eTrade.Data.Services.BannerService;
using eTrade.Data.Services.CategoryService;
using eTrade.Data.Services.DepartmentService;
using eTrade.Data.Services.HomeService;
using eTrade.Data.Services.ServiceService;
using eTrade.Data.Services.SubCategoryService;
using eTrade.Data.Services.TestimonialService;
using eTrade.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
            builder.Services.AddScoped<ISubCategoriesService, SubCategoryService>();
            builder.Services.AddScoped<IBannersService, BannersService>();
            builder.Services.AddScoped<IServicesService, ServicesService>();
            builder.Services.AddScoped<ITestimonialsService, TestimonialsService>();

            //user site
            builder.Services.AddScoped<IHomeService, HomeService>();


            //Auth
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });


            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

            app.Run();

        }
    }
}