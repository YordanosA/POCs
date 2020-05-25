using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Api.Filter
{
    public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _apiKey;
        public AuthorizationFilterAttribute(IConfiguration configuration)
        {
            _apiKey = configuration["ApiKey"];
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKeyHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (apiKeyHeader.Any())
            {
                if (!apiKeyHeader.Equals(_apiKey, StringComparison.CurrentCultureIgnoreCase))
                {
                    context.HttpContext.Response.StatusCode = 401;
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
            }
        }
    }
}
