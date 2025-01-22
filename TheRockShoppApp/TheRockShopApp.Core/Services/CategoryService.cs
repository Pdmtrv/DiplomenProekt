using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRockShopApp.Core.Contracts;
using TheRockShoppApp.Entities;
using TheRockShoppApp.Infrastructure.Data;

namespace TheRockShopApp.Core.Services
{
    public class CategoryService:ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category GetCategorieById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public List<Category> GetCategories()
        {
            List<Category > categories =  _context.Categories.ToList();
            return categories;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where (x => x.CategoryId == categoryId).ToList();
        }

    }
         
}
