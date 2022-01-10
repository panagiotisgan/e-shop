using eShop.Blazor.UI.Dto_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(HttpRequestMessage requestMessage);
        Task DeleteProduct(long productId);
        Task CreateOrUpdateProductAsync(Product product);
        Task<Product> GetByIdAsync(long productId);
    }
}
