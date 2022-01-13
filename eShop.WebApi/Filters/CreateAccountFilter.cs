using eShop.DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.WebApi.Filters
{
    public class CreateAccountFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            var userObject = context.ActionArguments["user"] as UserDTO;

            if (userObject != null && String.IsNullOrWhiteSpace(userObject.Email))
            {
                //context.ModelState.AddModelError("Email", "Email is required");
                context.Result = new BadRequestObjectResult("Email is required");
                return;
            }
        }
    }
}
