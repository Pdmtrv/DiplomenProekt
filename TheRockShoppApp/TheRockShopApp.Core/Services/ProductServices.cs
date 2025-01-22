using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRockShopApp.Core.Contracts;
using TheRockShoppApp.Entities;
using TheRockShoppApp.Infrastructure.Data;

namespace TheRockShopApp.Core.Services
{
    public class ProductServices : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductServices(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public bool Create(string name,  int categoryId,int manufacturerId,string productDescription, string picture, int quantity, decimal price, decimal  discount)
        {
            Product item = new Product
            {
                Name = name,
                Category = _context.Categories.Find( categoryId),
                manufacturer = _context.Manufacturers.Find(manufacturerId),
                ProductDescription = productDescription,
                Picture = picture,
                Quantity = quantity,
                Price = price,  
                Discount = discount        

            };
            _context.Products.Add(item);
            return _context.SaveChanges() != 0;
        }
        public Product GetProductsById(int productId)
        {
            return _context.Products.Find(productId);
        }
        public List<Product> GetPrdoducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public List<Product> GetProducts(string searchStringCategoryName, string searchStringManufacturerName)
        {
            List<Product> products = _context.Products.ToList();
            if (!string.IsNullOrEmpty(searchStringCategoryName) && !string.IsNullOrEmpty(searchStringManufacturerName))
            { 
                products = products.Where(x=>x.Category.Name.ToLower().Contains(searchStringCategoryName.ToLower()) 
                && x.manufacturer.Name.ToLower().Contains(searchStringManufacturerName.ToLower())).ToList();
            }

            else if(!string.IsNullOrEmpty(searchStringCategoryName))
            {
                products = products.Where(x => x.Category.Name.ToLower().Contains(searchStringCategoryName.ToLower())).ToList();
            }
            else if(!string.IsNullOrEmpty(searchStringManufacturerName))
            {
                products = products.Where(x => x.manufacturer.Name.ToLower().Contains(searchStringManufacturerName.ToLower())).ToList();
            }
            return products;
        }
   
       
        public Product GetProductById(int productId) 
        {
            return _context.Products.Find(productId);
        }

        public bool RemoveById(int productId)
        {
             var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            _context.Remove(product);
            return _context.SaveChanges() != 0;
        }
        public bool Update(int productId, string name, int categoryId, int manufacturerId, string productDescription, string picture, int quantity, decimal price, decimal discount)
        { 
            var product = GetProductById(productId); 
            if (product == default(Product))
            {
                return false;
            }
            product.Name = name;
            product.Category = _context.Categories.Find(categoryId);
            product.manufacturer = _context.Manufacturers.Find(manufacturerId);
            product.ProductDescription = productDescription;
            product.Picture = picture;
            product.Quantity = quantity;
            product.Price = price;
            product.Discount = discount;   
            _context.Update(product);
            return _context.SaveChanges() != 0;

        }

        
    }
}
