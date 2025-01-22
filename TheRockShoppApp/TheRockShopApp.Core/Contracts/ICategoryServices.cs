using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRockShoppApp.Entities;

namespace TheRockShopApp.Core.Contracts
{
    public interface ICategoryServices
    {
        List<Category> GetCategories();
        Category GetCategorieById(int categoryId);
        List<Product> GetProductsByCategory(int categoryId);
    }
}
