using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Globalization;
using System.Security.Claims;
using TheRockShopApp.Core.Contracts;
using TheRockShoppApp.Entities;
using TheRockShoppApp.Models.Order;

namespace TheRockShoppApp.Controllers
{
    [Authorize]
    public class OrderContrroller : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public OrderContrroller(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;                   
        }
        // GET: OrderContrroller
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<OrderIndexVM> orders = _orderService.GetOrders().Select(X => new OrderIndexVM
            {
                Id = X.Id,
                OrderDate = X.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                userId = X.UserId,
                User = X.User.UserName,
                ProductId= X.ProductId,
                Product = X.Product.Name,
                Picture = X.Product.Picture,
                Quantity = X.Product.Quantity,
                Price = X.Price,
                Discount = X.Discount,
                TotalPrice = X.TotalPrice,
            }).ToList();
            return View(orders);

        }

        // GET: OrderContrroller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderContrroller/Create
        public ActionResult Create(int Id)
        {
            Product product = _productService.GetProductsById(Id);
            if(product == null)
            {
                return NotFound();
            }
            OrderCreateVM order = new OrderCreateVM()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                QuantityInStock = product.Quantity,
                Price = product.Price,
                Discount = product.Discount,
                Picture = product.Picture,
            };
            return View(order);
            
            
             
        }

        // POST: OrderContrroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM bindingModel)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = this._productService.GetProductsById(bindingModel.ProductId);
            if(currentUserId == null || product == null ||product.Quantity < bindingModel.Quantity ||product.Quantity == 0)
            {
                return RedirectToAction("Denied", "Order");
                
            }
            if(ModelState.IsValid)
            {
                _orderService.Create(bindingModel.ProductId, currentUserId, bindingModel.Quantity);

            }
            return RedirectToAction("Index", "Product");
        }

        // GET: OrderContrroller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderContrroller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderContrroller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderContrroller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult  MyOrders( )
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<OrderIndexVM> orders = _orderService.GetOrdersByUser(currentUserId).Select(X => new OrderIndexVM
            {
                Id = X.Id,
                OrderDate = X.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                userId = X.UserId,
                User = X.User.UserName,
                ProductId = X.ProductId,
                Product = X.Product.Name,
                Picture = X.Product.Picture,
                Quantity = X.Product.Quantity,
                Price = X.Price,
                Discount = X.Discount,
                TotalPrice = X.TotalPrice,
            }).ToList();
            return View(orders);
        }

    }
}
