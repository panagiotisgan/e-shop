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
        [Route("GetByIds/{productIds}")]
        public async Task<IActionResult> GetByProductIds([FromRoute]string productIds)
        {
            var productIdArray = JsonConvert.DeserializeObject<long[]>(productIds);
            Images.Clear();
            foreach (var id in productIdArray)
            {
                try
                {
                    long actualId = Convert.ToInt64(id);
                    var image = await this._imageUnitOfWork.ImageDbRepository.GetImageByProductId(actualId);
                    if (image != null)
                        Images.Add(image);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Ok(Images);
        }


        [HttpGet]
        public IActionResult Version()
        {
            return Ok("version 1.0.0");
        }
    }
}
