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
    //[EnableCors("Policy")]
    //[Authorize(Roles = Role.MultipleRoles)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductUnitOfWork _productUnitOfWork;
        private readonly IImageUnitOfWork _imageUnitOfWork;
        public ProductsController(IProductUnitOfWork _productRepository, IImageUnitOfWork _imageUnitOfWork)
        {
            this._productUnitOfWork = _productRepository;
            this._imageUnitOfWork = _imageUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet]
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

        //[Authorize(Roles = Role.MultipleRoles)]
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
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
            catch (Exception ex)
            {
                throw ex;
            }

            return this.Ok();
        }
        
        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                this._productUnitOfWork.ProductRepository.UpdateEntity(product);
                this._productUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return this.Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery]long productId)
        {
            if (productId == 0)
                return BadRequest();

            try
            {
                var imageList = await this._imageUnitOfWork.ImageDbRepository.GetImagesByProductIdAsync(productId);
                if (imageList != null)
                {
                    foreach (var item in imageList)
                    {
                        this._imageUnitOfWork.ImageDbRepository.DeleteEntity(item.Id);
                    }
                }
            }
            catch(Exception ex)
            {

            }

            try
            {
                this._productUnitOfWork.ProductRepository.DeleteEntity(productId);
                this._productUnitOfWork.Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return Ok();
        }
    }
}
