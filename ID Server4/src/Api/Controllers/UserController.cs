using Api.Common;
using Api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private static List<User> users;

        public UserController()
        {
            if(users == null)
            {
                users = new List<User>();

                users.Add(new User { UserId = 1, FirstName = "Test 1" });
                users.Add(new User { UserId = 2, FirstName = "Test 2" });
            }
        }

        /// <summary>
        /// Returns all users of...
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(500)]
        public List<User> GetAll([FromQuery]PagingParams pagingParams)
        {
            return users;
        }

        /// <summary>
        /// Return a user based on user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        //[TypeFilter(typeof(AuthorizeCheckOperationFilter))]
        //[AuthorizeAttribute]
        [ApiExplorerSettings(GroupName = "v2")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public User GetById([FromQuery, BindRequired]int userId)
        {
            return users.SingleOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Add the new user provided
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">User created</response>
        /// <response code="400">User has missing/invalid values</response>
        /// <response code="500">Can't create the user right now</response>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult Create([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.UserId = users.Count() + 1;
            users.Add(user);

            return Ok(user);
        }
    }

}
