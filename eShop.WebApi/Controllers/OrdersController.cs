using eShop.DataAccess;
using eShop.DataAccess.DTOs;
using eShop.DataAccess.IServices;
using eShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IOrderDetailsUnitOfWork _orderDetailsUnitOfWork;
        private readonly IProductUnitOfWork _productUnitOfWork;
        public OrdersController(IOrderService orderService, IOrderUnitOfWork orderUnitOfWork, IOrderDetailsUnitOfWork orderDetailsUnitOfWork,
            IProductUnitOfWork productUnitOfWork)
        {
            _orderService = orderService;
            _orderUnitOfWork = orderUnitOfWork;
            _orderDetailsUnitOfWork = orderDetailsUnitOfWork;
            _productUnitOfWork = productUnitOfWork;
        }

        [HttpPost]
        [Route("placeorder")]
        //[Authorize]
        public IActionResult CreateOrder([FromBody] OrderDTO orderDTO)
        {
            if (_orderService.CreateOrder(orderDTO))
                return Ok("Order Placed Successfully");
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("GetOrders")]
        //[Authorize]
        public IActionResult GetOrders([FromQuery] int? page = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var orders = _orderDetailsUnitOfWork.OrderDetailsDbRepository.GetIquerableOrder();

                var ordersList = orders.Skip((page ?? 0) * pageSize)
                    .Take(pageSize)
                    .ToList();

                if (ordersList.Count > 0)
                    return Ok(new { list = ordersList, count = orders.Count() });                    
            }
            catch (Exception) { }

            return NoContent();
        }
    }
}
