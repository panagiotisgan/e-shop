using eShop.DataAccess.IRepositories;
using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess.Repositories
{
    public class OrderRepository : GenericRepository<Order, EshopDbContext>, IOrderRepository
    {
        public OrderRepository(EshopDbContext DbContext) :base(DbContext)
        {

        }
        public Order GetByOrderDate(DateTime orderDate,long userId)
        {
            return this._context.Orders.Include(o => o.OrderDetails)
                .Where(o => o.UserId == userId && (o.Order_Date.CompareTo(orderDate) == 0))
                .FirstOrDefault();
        }

        public IEnumerable<Order> GetByOrderStatus(OrderStatus orderStatus)
        {
            return this._context.Orders
                .Where(o => o.OrderStatus == orderStatus)
                .ToList();
        }

        public Order GetOrderByUserId(long userId)
        {
            return this._context.Orders.Include(o => o.OrderDetails)
                .Where(o => o.UserId == userId)
                .FirstOrDefault();
        }

        public IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate)
        {
            return this._context.Orders
                .Where(o => o.Order_Date.CompareTo(orderDate) == 0)
                .ToList();
        }
    }
}
