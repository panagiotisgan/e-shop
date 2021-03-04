using eShop.DataAccess.IRepositories;
using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class ImageRepository : GenericRepository<eShop.Model.Image,EshopDbContext>, IImageDbRepository
    {
        public ImageRepository(EshopDbContext context): base(context)
        {

        }

        public async Task<List<Image>> GetImagesByProductIdAsync(long productId)
        {
            return await this._context.Images.Where(im => im.ProductId == productId).ToListAsync();
        }

        public async Task<Image> GetImageByProductId(long productId)
        {
            return await this._context.Images.FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }

    public interface IImageDbRepository : IDbRepository<eShop.Model.Image>,IImageRepository
    {
        Task<List<Image>> GetImagesByProductIdAsync(long productId);
        Task<Image> GetImageByProductId(long productId);
    }
}
