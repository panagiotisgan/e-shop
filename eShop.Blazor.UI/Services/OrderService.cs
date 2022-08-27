using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class OrderService : IOrderService
    {
        private HttpClient client;
        private ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        //public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public IEnumerable<OrderDetails> OrdersList { get; set; }
        public OrderService(HttpClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = client;
            _localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<(OrderPaginationDTO orderList,PaginationMetaDataResult paginationData)> GetOrdersAsync(int page = 1)
        {
            var token = await _localStorageService.GetItemAsync<string>("jwt_token");
            OrdersList = new List<OrderDetails>();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = await client.GetAsync($"api/Orders/GetOrders?pageNum={page}&pageSize={PageSize}");
           
            var metadataExist = result.Headers.TryGetValues("X-Pagination", out var paginationValues);
            var res = paginationValues.Count()>0 ? paginationValues.FirstOrDefault():"";
            //result.EnsureSuccessStatusCode();
            try {
                OrderPaginationDTO orderPaginationDto = JsonConvert.DeserializeObject<OrderPaginationDTO>(await result.Content.ReadAsStringAsync());
                //Console.WriteLine(orderPaginationDto.count);
                OrdersList = orderPaginationDto.list;
                return (orderPaginationDto, JsonConvert.DeserializeObject<PaginationMetaDataResult>(res));
            }
            catch (Exception ex) { }
            return (null,null);
             
        }
    }
}
