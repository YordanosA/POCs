using Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace Api.Filter
{
    public class AdminActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            var user = context.HttpContext.User.Claims.Where(c => c.Value == AppConfiguration.BandlayAdmin).FirstOrDefault();

            if(user == null)
            {
                //var data = new
                //{
                //    Success = false,
                //    Message = "You are not authorized to access this resource.",
                //    HttpStatusCode.Unauthorized
                //};

                //context.HttpContext.Response.StatusCode = 401;
                //context.Result = new JsonResult(data);

                context.Result = new RedirectResult("Auth/NotAuthorized");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.

        }
    }
}
