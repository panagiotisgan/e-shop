using eShop.DataAccess.IRepositories;
using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess
{
    public class OrderDetailsRepository : GenericRepository<OrderDetails,EshopDbContext>, IOrderDetailsDbRepository
    {
        public OrderDetailsRepository(EshopDbContext context) :base(context)
        {
        }
        public List<OrderDetails> GetDetailsByOrderId(long orderId)
        {
            
            return this._context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToList();
        }


        public List<OrderDetails> GetDetailsByOrderId(List<long> orderIds)
        {
           return _context.OrderDetails.Where(x=> orderIds.Contains(x.OrderId)).ToList();
        }


        public IQueryable<OrderDetails> GetIquerableOrder()
        {
            return from orderDet in this._context.OrderDetails.Include(y=>y.Product)
                   join order  in this._context.Orders.Include(or=>or.User)
                   on orderDet.OrderId equals order.Id
                   select  orderDet ;
        }
    }

    public interface IOrderDetailsDbRepository : IDbRepository<OrderDetails>, IOrderDetailsRepository
    {
        List<OrderDetails> GetDetailsByOrderId(long orderId);
        List<OrderDetails> GetDetailsByOrderId(List<long> orderIds);

        IQueryable<OrderDetails> GetIquerableOrder();
    }
}
