using eShop.DataAccess.DTOs;
using eShop.DataAccess.Helpers;
using eShop.DataAccess.IServices;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess
{
    public class OrderService : IOrderService
    {
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IOrderDetailsUnitOfWork _orderDetailsUnitOfWork;
        private readonly IProductUnitOfWork _productUnitOfWork;
        public OrderService( IOrderDetailsUnitOfWork _orderDetailsUnitOfWork,
            IOrderUnitOfWork orderUnitOfWork, IProductUnitOfWork productUnitOfWork)
        {
            this._orderUnitOfWork = orderUnitOfWork;
            this._orderDetailsUnitOfWork = _orderDetailsUnitOfWork;
            this._productUnitOfWork = productUnitOfWork;
        }

        public bool CreateOrder(OrderDTO orderDTO)
        {
            decimal totalCost = 0;
            List<long> productsIds = new List<long>();

            if(orderDTO != null && orderDTO.OrderDetailsDTOList.Count > 0)
            {
                totalCost = Calculator.CalculateOrderTotalCost(orderDTO.OrderDetailsDTOList);
                productsIds = orderDTO.OrderDetailsDTOList.Select(x => x.ProductId).ToList();
            }
             

            var querableProducts = _productUnitOfWork.ProductRepository.GetQueryable().Where(x => productsIds.Contains(x.Id)).ToList();
            
            try
            {

                _orderUnitOfWork.StartTransaction();

                Order order = new Order()
                {
                    Invoice = orderDTO.Invoice,
                    OrderStatus = OrderStatus.Pending,
                    OrderDate = DateTime.Now,
                    TotalCost = totalCost,
                    DeliveredDate = null,
                    UserId = orderDTO.UserId
                };

                _orderUnitOfWork.OrderDdRepository.CreateEntity(order);
                _orderUnitOfWork.OrderDdRepository.Save();

                foreach (var orderDet in orderDTO.OrderDetailsDTOList)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        OrderId = order.Id,
                        ProductId = orderDet.ProductId,
                        Quantity = orderDet.Quantity
                    };

                    var product = querableProducts.FirstOrDefault(x => x.Id == orderDet.ProductId);
                    var stockQty = product.StockQty - orderDet.Quantity;
                    
                    product.StockQty = stockQty;
                    product.SetInStock(stockQty);

                    _orderDetailsUnitOfWork.OrderDetailsDbRepository.CreateEntity(orderDetails);

                    _productUnitOfWork.ProductRepository.UpdateEntity(product);


                    _productUnitOfWork.Save();
                    _orderUnitOfWork.Save();
                    
                }
                _orderUnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _orderUnitOfWork.Rollback();
                throw new Exception("Order error",ex.InnerException);                
            }
        }
    }
}
