using eShop.Blazor.UI.Services;
using eShop.Blazor.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace eShop.Blazor.UI.Helpers
{
    public static class HttpClientRegister
    {
        public static void ServicesRegister(this IServiceCollection serviceCollection, string baseUrl)
        {
            //Error me provlima sto registration to httpClient me ViewModel
            serviceCollection.AddHttpClient<IProductViewModel, ProductViewModel>(client =>
            client.BaseAddress = new Uri(baseUrl));
            serviceCollection.AddHttpClient<IOrderService, OrderService>(client =>
             client.BaseAddress = new Uri(baseUrl));
            serviceCollection.AddHttpClient<IUserService, UserService>(client =>
             client.BaseAddress = new Uri(baseUrl));
            serviceCollection.AddHttpClient<ICategoryService, CategoryService>(client =>
                 client.BaseAddress = new Uri(baseUrl));
            serviceCollection.AddHttpClient<IImageService, ImageService>(client =>
                client.BaseAddress = new Uri(baseUrl));
            serviceCollection.AddHttpClient<IProductService, ProductService>(client =>
                client.BaseAddress = new Uri(baseUrl));
        }
    }
}
