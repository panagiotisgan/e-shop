using eShop.Blazor.UI.Dto_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task DeleteProduct(long productId);
        Task CreateProductAsync(Product product);
    }
}
