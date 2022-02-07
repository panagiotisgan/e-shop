using eShop.WebApi.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace eShop.WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("bearer ", "", StringComparison.OrdinalIgnoreCase);
            //context.HttpContext.
            string[] split = token.Split(".");


            var res = new JwtSecurityToken(token);

            var date = res.ValidTo;
            var dr = DateTime.Now;
            if (res.ValidTo < DateTime.Now)
            {
                throw new Exception("Jwt Token was expired");
            }
        }
    }

    class JwtDecoded
    {
        public string iat { get; set; }
        public string name { get; set; }
        public string sub { get; set; }
    }
}
