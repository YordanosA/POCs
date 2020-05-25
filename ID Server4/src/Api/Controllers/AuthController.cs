using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("Auth")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : ControllerBase
    {
        [HttpGet("NotAuthorized")]
        [ProducesResponseType(401)]
        public async Task NotAuthorized()
        {
            HttpContext.Response.StatusCode = 401;
            var jsonString = "{\"success\":false,\"message\":\"You are not authorized to access this resource\"}";
            byte[] data = Encoding.UTF8.GetBytes(jsonString);
            Response.ContentType = "application/json";
            await Response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}
