using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.ViewModels
{
    public class OrderViewModel : IOrderViewModel
    {
        private HttpClient client;
        private ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<OrderDetails> OrdersList { get; set; }
        public OrderViewModel(HttpClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = client;
            _localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task GetOrdersAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("jwt_token");
            OrdersList = new List<OrderDetails>();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = await client.GetAsync($"api/Orders/GetOrders?page={Page}&pageSize={PageSize}");
            result.EnsureSuccessStatusCode();
            OrdersList = JsonConvert.DeserializeObject<List<OrderDetails>>(await result.Content.ReadAsStringAsync());
        }
    }
}
