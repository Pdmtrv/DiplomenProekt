using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheRockShopApp.Core.Contracts;
using TheRockShopApp.Core.Services;
using TheRockShoppApp.Entities;
using TheRockShoppApp.Infrastructure.Data;
using TheRockShoppApp.Infrastructure.Data.Infrastructure;


namespace TheRockShoppApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseLazyLoadingProxies()
               .UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            }) 
            .AddRoles<IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<ICategoryServices, CategoryService>();
            builder.Services.AddTransient<IProductService,  ProductServices>();
            builder.Services.AddTransient<IManifacturerServices, ManufacturerService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            var app = builder.Build();
            app.PrepareDatbase();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}