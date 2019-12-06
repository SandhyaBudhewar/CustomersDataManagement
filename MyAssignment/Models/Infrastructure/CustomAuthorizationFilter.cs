using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAssignment.Models.Infrastructure
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context.HttpContext.Session.GetString("username") == null)
            {
                context.Result = new RedirectResult("~/Home/InvalidSession");
            }
        }

    }
}
