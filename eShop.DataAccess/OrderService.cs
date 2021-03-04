using eShop.DataAccess.DTOs;
using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        public OrderService(IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            this._orderRepository = orderRepository;
            this._orderDetailsRepository = orderDetailsRepository;
        }


        //Είναι λάθος. Το Order και Order Details πρέπει να στέλνονται με ένα save στην βάση
        //στο Save στο order repository
        public bool CreateOrder(OrderDTO orderDTO)
        {
            decimal totalCost = 0;

            foreach (var detail in orderDTO.orderDetailsDTOs)
            {
                totalCost += detail.Quantity * detail.UnitPrice;
            }

            try
            {

                Order order = new Order()
                {
                    Invoice = orderDTO.Invoice,
                    OrderStatus = OrderStatus.Pending,
                    Order_Date = DateTime.Now,
                    Total_Cost = totalCost,
                    Delivered_Date = null
                };

                _orderRepository.CreateEntity(order);
                _orderRepository.Save();

                foreach (var orderDet in orderDTO.orderDetailsDTOs)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        OrderId = order.Id,
                        ProductId = orderDet.ProductId,
                        Product_Name = orderDet.ProductName,
                        Quantity = orderDet.Quantity,
                        Unit_Price = orderDet.UnitPrice
                    };
                    _orderDetailsRepository.CreateEntity(orderDetails);
                    _orderDetailsRepository.Save();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Order error",ex.InnerException);
            }
        }
    }
}
