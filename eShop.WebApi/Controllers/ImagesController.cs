using eShop.DataAccess;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.WebApi.Controllers
{
    //[EnableCors("Policy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageUnitOfWork _imageUnitOfWork;
        private List<Model.Image> Images;
        public ImagesController(IImageUnitOfWork imageUnitOfWork)
        {
            this._imageUnitOfWork = imageUnitOfWork;
            this.Images = new List<Model.Image>();
        }

        [HttpGet]
        [Route("GetById/{productId}")]
        public async Task<IActionResult> GetByProductId([FromRoute] string productId)
        {
            Int64.TryParse(productId, out var imageId);
            Images.Clear();
            try
            {
                var image = await this._imageUnitOfWork.ImageDbRepository.GetImagesByProductIdAsync(imageId);
                if (image != null)
                {
                    Images.AddRange(image);
                    return Ok(Images);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return NotFound();
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _imageUnitOfWork.ImageDbRepository.DeleteEntity(id);
                _imageUnitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult Version()
        {
            return Ok("version 1.0.0");
        }
    }
}
