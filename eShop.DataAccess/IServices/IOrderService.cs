using eShop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IServices
{
    public interface IOrderService
    {
        public bool CreateOrder(OrderDTO orderDTO);
    }
}
