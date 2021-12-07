using eShop.DataAccess;
using eShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryUnitOfWork _categoryUnitOfWork;
        public CategoriesController(ICategoryUnitOfWork categoryUnitOfWork)
        {
            this._categoryUnitOfWork = categoryUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await this._categoryUnitOfWork.CategoryDbRepository.GetAllAsync();
                return Ok(categories);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
