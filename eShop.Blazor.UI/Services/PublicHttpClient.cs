using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class PublicHttpClient
    {
        public HttpClient _client { get; private set; }
        public PublicHttpClient(HttpClient client)
        {
            this._client = client;
        }
    }
}
