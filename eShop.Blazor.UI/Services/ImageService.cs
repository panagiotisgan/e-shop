using eShop.Blazor.UI.Dto_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class ImageService : IImageService
    {
        private HttpClient _client;
        public ImageService(HttpClient client)
        {
            _client = client;
        }
        public async Task DeleteImageAsync(long id)
        {
            try
            {
                await _client.DeleteAsync($"api/Images/{id}");
            }
            catch(Exception ex)
            {

            }
        }

        public async Task<IEnumerable<Image>> GetImagesAsync(long productId)
        {
            var httpResponseMessage = await _client.GetAsync($"api/Images/GetById/{productId}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Image>>(content);
                return result;
            }

            return null;
        }
    }
}
