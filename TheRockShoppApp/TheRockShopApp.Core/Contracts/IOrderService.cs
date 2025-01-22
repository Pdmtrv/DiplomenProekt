using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRockShoppApp.Entities;

namespace TheRockShopApp.Core.Contracts
{
    public interface IOrderService
    {
        bool Create(int productId, string userId, int quantity);
        List<Order> GetOrders();
         public List <Order> GetOrdersByUser(string userId);
    }
}
