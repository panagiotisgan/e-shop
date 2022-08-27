using eShop.DataAccess;
using eShop.DataAccess.DTOs;
using eShop.DataAccess.IServices;
using eShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult GetOrders([FromQuery] int? pageNum = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var orders = _orderDetailsUnitOfWork.OrderDetailsDbRepository.GetOrderDetailsPaged(pageNum.Value, pageSize);

                var metadata = new
                {
                    orders.PageSize,
                    orders.TotalCount,
                    orders.TotalPages,
                    orders.HasNext,
                    orders.HasPrevious,
                    orders.CurrentPage
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                if(orders.Count == 0)
                    return NoContent();

                return Ok(new { list = orders });
            }
            catch (Exception) { }

            return NoContent();
        }
    }
}
