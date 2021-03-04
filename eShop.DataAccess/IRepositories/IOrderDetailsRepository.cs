using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IRepositories
{
    public interface IOrderDetailsRepository:IBaseRepository<OrderDetails>
    {
        OrderDetails GetOrderDetailsByName(string productName);
        List<OrderDetails> GetDetailsByOrderId(long orderId);
    }
}
