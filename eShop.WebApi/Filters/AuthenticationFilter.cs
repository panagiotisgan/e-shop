using eShop.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.WebApi.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authenticationModel = context.ActionArguments["authenticateModel"] as AuthenticateCredentials;
            if(String.IsNullOrWhiteSpace(authenticationModel.Password))
            {
                //context.ModelState.AddModelError("Password", "Password is required.");
                context.Result = new BadRequestObjectResult("Password is required.");
                return;
            }

            if (String.IsNullOrWhiteSpace(authenticationModel.UserName))
            {
                //context.ModelState.AddModelError("Username", "Username is required.");
                context.Result = new BadRequestObjectResult("Username is required.");
                return;

            }

            if (!authenticationModel.IsHuman)
            {
                //context.ModelState.AddModelError("IsHuman", "Please verify that you are an actual user.");
                context.Result = new BadRequestObjectResult("Please verify that you are an actual user.");
                return;
            }
        }
    }
}
