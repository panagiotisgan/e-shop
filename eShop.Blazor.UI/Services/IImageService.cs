using eShop.Blazor.UI.Dto_Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IImageService
    {
        Task DeleteImageAsync(long id);
        Task<IEnumerable<Image>> GetImagesAsync(long productId);
    }
}
