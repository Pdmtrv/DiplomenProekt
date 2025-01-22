using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRockShoppApp.Entities;

namespace TheRockShopApp.Core.Contracts
{
    public interface IProductService
    {
        bool Create(string name, int categoryId, int manufacturerId, string productDescription, string picture, int quantity, decimal price, decimal discount);

        bool Update(int productId,string name, int categoryId, int manufacturerId, string productDescription, string picture, int quantity, decimal price, decimal discount);
        List<Product> GetPrdoducts();

        bool RemoveById(int productId);
        List<Product> GetProducts(string searchStringCategoryName, string searchStringManufacturerName);

        Product  GetProductsById(int  productId);
         
    }
}
