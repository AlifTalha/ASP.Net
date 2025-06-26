using BLL.DTOs;
using BLL.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace IntroTier.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(UserDTO user)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            bool success = UserService.Register(user);
            if (success)
            {
                // Attempt auto-login
                var loggedInUser = UserService.Login(user.Username, user.Password);
                if (loggedInUser != null)
                {
                    if (HttpContext.Current == null || HttpContext.Current.Session == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Session is not available.");
                    }

                    HttpContext.Current.Session["user"] = loggedInUser;
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Registration and login successful");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Username already exists");
            }
        }



        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(UserDTO user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Username and password are required.");
            }

            var result = UserService.Login(user.Username, user.Password);
            if (result != null)
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Session is not available.");
                }

                HttpContext.Current.Session["user"] = result;
                return Request.CreateResponse(HttpStatusCode.OK, "Login successful");
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid credentials");
        }


        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout()
        {
            HttpContext.Current.Session.Abandon();
            return Request.CreateResponse(HttpStatusCode.OK, "Logged out");
        }

        [HttpGet]
        [Route("me")]
        public HttpResponseMessage CurrentUser()
        {
            var user = HttpContext.Current.Session["user"] as UserDTO;
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}
