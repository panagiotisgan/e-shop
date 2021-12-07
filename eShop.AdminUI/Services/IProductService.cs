using eShop.AdminUI.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.AdminUI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
