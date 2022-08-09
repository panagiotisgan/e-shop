﻿using eShop.DataAccess.IRepositories;
using eShop.Model;
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

        public OrderDetails GetOrderDetailsByName(string productName)
        {
            return this._context.OrderDetails
                .Where(od => od.Product_Name.Equals(productName))
                .FirstOrDefault();
        }
    }

    public interface IOrderDetailsDbRepository : IDbRepository<OrderDetails>, IOrderDetailsRepository
    {
        List<OrderDetails> GetDetailsByOrderId(long orderId);
        OrderDetails GetOrderDetailsByName(String productName);
    }
}