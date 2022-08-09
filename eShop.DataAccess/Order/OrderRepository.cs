using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class OrderRepository : GenericRepository<Order, EshopDbContext>, IOrderDbRepository
    {
        public OrderRepository(EshopDbContext context) : base(context)
        {
        }

        public Order GetByOrderDate(DateTime orderDate, long userId)
        {
            return this._context.Orders.Include(o => o.OrderDetails)
                .Where(o => o.UserId == userId && (o.OrderDate.CompareTo(orderDate) == 0))
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
                .Where(o => o.OrderDate.CompareTo(orderDate) == 0)
                .ToList();
        }
    }

    public interface IOrderDbRepository : IDbRepository<Order>, IOrderRepository
    {
        Order GetOrderByUserId(long UserId);
        Order GetByOrderDate(DateTime orderDate, long userId);
        IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate);
        IEnumerable<Order> GetByOrderStatus(OrderStatus orderStatus);
    }
}
