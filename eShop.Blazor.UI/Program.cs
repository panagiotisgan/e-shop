using Blazored.LocalStorage;
using Blazored.Modal;
using eShop.Blazor.UI.Helpers;
using eShop.Blazor.UI.Services;
using eShop.Blazor.UI.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
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
            var baseUrl = builder.Configuration["BaseUrl"];

            builder.Services.AddOptions();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            //builder.Services.AddSingleton<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<AuthenticationStateProvider, JwtTokenAuthenticationStateProvider>();
            builder.Services.AddSingleton<HttpStatusCodeHandler>();

            //builder.Services.AddHttpClient<IProductService, ProductService>(client =>
            // client.BaseAddress = new Uri("https://localhost:44371/"));
                        

            builder.Services.ServicesRegister(baseUrl);


            //builder.Services.AddHttpClient<IAuthenticateService, AuthenticateService>(client => 
            //client.BaseAddress = new Uri("https://localhost:44371/"));



            builder.Services.AddBlazoredModal();

            await builder.Build().RunAsync();
        }       
    }

   

}
