using Api.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    [Route("identity")]
    [Authorize]
    [TypeFilter(typeof(AdminActionFilter))]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public class IdentityController : ControllerBase
    {
        /// <summary>
        /// This method returns loggedin user claims
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
