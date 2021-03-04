using eShop.DataAccess;
using eShop.DataAccess.IRepositories;
using eShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.WebApi.Controllers
{
    [EnableCors("Policy")]
    [Authorize(Roles = Role.MultipleRoles)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductUnitOfWork _productUnitOfWork;
        public ProductsController(IProductUnitOfWork _productRepository)
        {
            this._productUnitOfWork = _productRepository;
        }

        [AllowAnonymous]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await this._productUnitOfWork.ProductRepository.GetAllAsync();
            return this.Ok(products);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetProduct/{productId}")]
        public async Task<IActionResult> GetByIdAsync(long productId)
        {
            return this.Ok(await this._productUnitOfWork.ProductRepository.GetByIdAsync(productId));
        }

        [Authorize(Roles = Role.MultipleRoles)]
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                this._productUnitOfWork.ProductRepository.CreateEntity(product);
                this._productUnitOfWork.Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return this.Ok();
        }
    }
}
