

using BLL.DTOs;
using BLL.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TieePMS.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/user/register")]
        public HttpResponseMessage Register(UserDTO user)
        {
            var result = UserService.Register(user);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, "Registration successful");
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Registration failed");
        }

        [HttpPost]
        [Route("api/user/login")]
        public HttpResponseMessage Login(UserDTO user)
        {
            var result = UserService.Login(user.Username, user.Password);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid credentials");
        }

        [HttpPost]
        [Route("api/user/logout")]
        public HttpResponseMessage Logout()
        {
            var authHeader = Request.Headers.Authorization;
            if (authHeader != null && authHeader.Scheme == "Bearer")
            {
                var token = authHeader.Parameter;
                var result = UserService.InvalidateSessionToken(token);

                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User logged out successfully.");
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "No valid session found for logout.");
        }
    }
}
