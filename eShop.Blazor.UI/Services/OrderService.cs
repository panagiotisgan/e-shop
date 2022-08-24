using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class OrderService : IOrderService
    {
        private HttpClient client;
        private ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public IEnumerable<OrderDetails> OrdersList { get; set; }
        public OrderService(HttpClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = client;
            _localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<OrderPaginationDTO> GetOrdersAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("jwt_token");
            OrdersList = new List<OrderDetails>();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = await client.GetAsync($"api/Orders/GetOrders?pageNum={Page}&pageSize={PageSize}");
            result.EnsureSuccessStatusCode();
            try {
                OrderPaginationDTO orderPaginationDto = JsonConvert.DeserializeObject<OrderPaginationDTO>(await result.Content.ReadAsStringAsync());
                //Console.WriteLine(orderPaginationDto.count);
                OrdersList = orderPaginationDto.list;
                return orderPaginationDto;
            }
            catch (Exception ex) { }
            return null;
             
        }
    }
}
