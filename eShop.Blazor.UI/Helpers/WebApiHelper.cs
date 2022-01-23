using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Helpers
{
    public class WebApiHelper : IWebApiHelper
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly ITokenRepository _tokenRepository;
        public WebApiHelper(HttpClient client, string baseUrl, ITokenRepository tokenRepository)
        {
            _client = client;
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _baseUrl = baseUrl;
            _tokenRepository = tokenRepository;
        }
        public Task InvokeDelete(string uri)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> InvokeGet<T>(string uri)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> InvokePost<T>(string uri, T obj)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> InvokePostReturnString<T>(string uri, T obj)
        {
            await AddTokenHeader();
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var result = await _client.PostAsync(new Uri(GetUrl(uri)), content);
                if (result.IsSuccessStatusCode)
                {
                    var res = await result.Content.ReadFromJsonAsync<string>();
                    return res;
                }
            }
            catch (Exception ex)
            {

            }           

            return null;
        }

        public Task InvokePut<T>(string uri, T obj)
        {
            throw new System.NotImplementedException();
        }

        private string GetUrl(string uri)
        {
            var url = $"{_baseUrl}/{uri}";
            return url;
        } 

        private async Task AddTokenHeader()
        {
            if (_tokenRepository != null && !string.IsNullOrWhiteSpace(await _tokenRepository.GetToken()))
            {
                _client.DefaultRequestHeaders.Remove("jwtToken");
                _client.DefaultRequestHeaders.Add("jwtToken", await _tokenRepository.GetToken());
            }
        }
    }
}
