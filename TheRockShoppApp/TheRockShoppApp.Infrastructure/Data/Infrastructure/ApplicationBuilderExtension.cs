using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRockShoppApp.Entities;

namespace TheRockShoppApp.Infrastructure.Data.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatbase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            await RoleSeeder(services);
            await SeedAdministrator(services);
            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);
            var dataManufacturer = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
           SeedManufacturers(dataManufacturer);
            return app;
        }
        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.UserName = "admin";
                user.Email = "admin@admin.com";
                user.Address = "admin address";
                user.PhoneNumber = "08888888888";
                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category {Name = "Crystals and Minerals"},
                new Category {Name = "Geodes"},
                new Category {Name = "Druizes"},
                new Category {Name = "Jewlery"},
                new Category {Name = "Braceletes"},
            }); 
            dataCategory.SaveChanges();
        }
        private static void SeedManufacturers (ApplicationDbContext dataManufacturer)
        {
            if (dataManufacturer.Manufacturers.Any())
            {
                return;
            }
            dataManufacturer.Manufacturers.AddRange(new[]
{
                new Manufacturer {Name = "Brazil"},
                new Manufacturer {Name = "China"},
                new Manufacturer  {Name = "India"},
                new Manufacturer {Name = "Russia"},
                new Manufacturer {Name = "USA"},
            });
            dataManufacturer.SaveChanges();
        }
        public static async Task<IApplicationBuilder> PrepareDatabase (this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataManufacturer = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataManufacturer);

            return app;
        }

    }
}
