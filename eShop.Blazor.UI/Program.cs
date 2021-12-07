using Blazored.Modal;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Blazor.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddHttpClient<IProductService, ProductService>(client =>
             client.BaseAddress = new Uri("https://localhost:44371/"));
            //builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddHttpClient<IUserService, UserService>(client =>
             client.BaseAddress = new Uri("https://localhost:44371/"));
            builder.Services.AddHttpClient<ICategoryService, CategoryService>(client =>
                 client.BaseAddress = new Uri("https://localhost:44371/"));

            builder.Services.AddBlazoredModal();

            //builder.Services.AddControllersWithViews()

            await builder.Build().RunAsync();
        }       
    }

   

}
