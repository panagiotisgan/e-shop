using eShop.DataAccess;
using eShop.DataAccess.IRepositories;
using eShop.Model;
using eShop.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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

        [HttpGet]
        [Route("GetProducts")]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            var products = await this._productUnitOfWork.ProductRepository.GetAllAsync();
            return this.Ok(products);
        }

        [Authorize]
        [HttpGet]
        [Route("GetProduct/{productId}")]
        public async Task<IActionResult> GetByIdAsync(long productId)
        {
            //return this.Ok(await this._productUnitOfWork.ProductRepository.GetByIdAsync(productId));
            return this.Ok(await this._productUnitOfWork.ProductRepository.GetProductAsync(productId));
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
            catch (Exception)
            {
                throw;
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
                //Get the entity from db to enable tracking
                var dbEntity = _productUnitOfWork.ProductRepository.GetById(product.Id);
                
                dbEntity.StockQty = product.StockQty;
                dbEntity.Price = product.Price;
                dbEntity.CategoryId = product.CategoryId;
                dbEntity.Code = product.Code;
                dbEntity.InStock = product.InStock;
                dbEntity.Name = product.Name;
                dbEntity.Images = product.Images;

                this._productUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return this.Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] long productId)
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
            catch (Exception)
            {

            }

            try
            {
                this._productUnitOfWork.ProductRepository.DeleteEntity(productId);
                this._productUnitOfWork.Save();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }


        //private List<string> GetChangedProperties<T>(object A, object B)
        //{
        //    if (A != null && B != null)
        //    {
        //        var type = typeof(T);
        //        var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //        var allSimpleProperties = allProperties.Where(pi => pi.PropertyType == );
        //        var unequalProperties =
        //               from pi in allSimpleProperties
        //               let AValue = type.GetProperty(pi.Name).GetValue(A, null)
        //               let BValue = type.GetProperty(pi.Name).GetValue(B, null)
        //               where AValue != BValue && (AValue == null || !AValue.Equals(BValue))
        //               select pi.Name;
        //        return unequalProperties.ToList();
        //    }
        //    else
        //    {
        //        throw new ArgumentNullException("You need to provide 2 non-null objects");
        //    }
        //}
    }


}
