using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    internal class InitializeClient : IHttpClientBuilder
    {
        public string Name => throw new NotImplementedException();


        public IServiceCollection Services => Services.AddScoped<IUserService, UserService>();

        //public static IHttpClientBuilder HttpClientCreate()
        //{
        //    IHttpClientBuilder client = new IHttpClientBuilder()
        //    {
        //        BaseAddress = new Uri("https://localhost:44371/")
        //    };

        //    return client;
        //}
    }
}
