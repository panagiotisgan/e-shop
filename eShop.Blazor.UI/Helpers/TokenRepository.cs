using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Helpers
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime _jSRuntime;
        public TokenRepository(IJSRuntime jSRuntime)
        {
            this._jSRuntime = jSRuntime;
        }
        
        public async Task<string> GetToken()
        {
            return await _jSRuntime.InvokeAsync<string>("sessionStorage.getItem", "jwtToken");
        }

        public async Task SetToken(string token)
        {
            await _jSRuntime.InvokeVoidAsync("sessionStorage.setItem", "jwtToken", token);
        }
    }
}
