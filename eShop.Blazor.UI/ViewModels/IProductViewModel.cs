using eShop.Blazor.UI.Dto_Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.ViewModels
{
    public interface IProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        Task CreateOrUpdateProductAsync(Product product);
        //Task DeleteProduct(long productId);
        void DeleteProduct();
        Task<Product> GetByIdAsync(long productId);
        Task GetProductsAsync();
    }
}