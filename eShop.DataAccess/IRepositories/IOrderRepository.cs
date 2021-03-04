using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IRepositories
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        Order GetOrderByUserId(long UserId);
        Order GetByOrderDate(DateTime orderDate,long userId);
        IEnumerable<Order> GetOrdersByOrderDate(DateTime orderDate);
        IEnumerable<Order> GetByOrderStatus(OrderStatus orderStatus);
    }
}
