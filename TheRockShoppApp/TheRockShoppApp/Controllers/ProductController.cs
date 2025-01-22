using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheRockShopApp.Core.Contracts;
using TheRockShoppApp.Entities;
using TheRockShoppApp.Models.Category;
using TheRockShoppApp.Models.Manufacturer;
using TheRockShoppApp.Models.Product;

namespace TheRockShoppApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IManifacturerServices _manifacturerServices;
        private readonly ICategoryServices _categoryService;
        public ProductController(IProductService productService, IManifacturerServices manifacturerServices, ICategoryServices categoryServices )
        {
            _productService = productService;
            _manifacturerServices = manifacturerServices;
            _categoryService = categoryServices;
        }

        // GET: ProductController
        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringManufacturerName)
        {
            List<ProductIndexVM> products = _productService.GetProducts(searchStringCategoryName, searchStringManufacturerName).Select(product => new ProductIndexVM
            {
                Id = product.Id,
                ProductName = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                ManufacturerId = product.ManufacturerId,
                ManufacturerName = product.manufacturer.Name,
                ProductDescription = product.ProductDescription,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount

            }).ToList();

            return this.View(products);
        }

        // GET: ProductController/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = _productService.GetProductsById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.Name,
                CategoryId = item.CategoryId,
                 CategoryName = item.Category.Name,
                ManufacturerId = item.ManufacturerId,
                 ManufacturerName = item.manufacturer.Name,
                ProductDescription = item.ProductDescription,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item .Discount
            };
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Manufacturer = _manifacturerServices.GetManufacturers().Select(x => new ManufacturerPairVM()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            product.Categories = _categoryService.GetCategories().Select(x => new CategoryPairVM()
            {
                Id = x.Id,
                Name = x.Name
            }
            ).ToList();
            return View(product);
            
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]ProductCreateVM product )
        {
            if(ModelState.IsValid)
            {
                 var createdId = _productService.Create( product.Name, product.CategoryId, product.ManufacturerId,product.ProductDescription, product.Picture,
                     product.Quantity,product.Price, product.Discount );
                if(createdId)
                {
                    return  RedirectToAction(nameof (Index));
                }
                
            }
            return View();

        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
             Product product = _productService.GetProductsById(id);
            if(product == null)
            {
                return NotFound();
            }
            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                // CategoryName = product.Category.Name,
                ManufacturerId = product.ManufacturerId,
                // ManufacturerName = product.manufacturer.Name,
                ProductDescription = product.ProductDescription,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount

            };
                        updatedProduct.Manufacturer = _manifacturerServices.GetManufacturers().Select(x => new ManufacturerPairVM()
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToList();
            updatedProduct.Categories = _categoryService.GetCategories().Select(x => new CategoryPairVM()
            {
                Id = x.Id,
                Name = x.Name
            }
            ).ToList();
            return View(updatedProduct);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var  updated = _productService.Update(id,product.Name, product.CategoryId, product.ManufacturerId, product.ProductDescription, product.Picture,
                    product.Quantity, product.Price, product.Discount);
                if (updated)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(product);

        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {


            Product item = _productService.GetProductsById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.Name,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name,
                ManufacturerId = item.ManufacturerId,
                ManufacturerName = item.manufacturer.Name,
                ProductDescription = item.ProductDescription,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        { 
            var deleted = _productService.RemoveById(id);
            if(deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
