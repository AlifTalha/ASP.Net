//using BLL.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace TieePMS.Controllers
//{
//    public class CategoryController : ApiController
//    {

//        [HttpGet]
//        [Route("api/category/all")]
//        public HttpResponseMessage Get()
//        {
//            var data = CategoryService.Get();
//            return Request.CreateResponse(HttpStatusCode.OK, data);
//        }
//        [HttpGet]
//        [Route("api/category/{id}")]
//        public HttpResponseMessage Get(int id)
//        {
//            var data = CategoryService.Get(id);
//            return Request.CreateResponse(HttpStatusCode.OK, data);
//        }
//        [HttpGet]
//        [Route("api/category/{id}/products")]
//        public HttpResponseMessage GetwithProducts(int id)
//        {
//            var data = CategoryService.GetwithProducts(id);
//            return Request.CreateResponse(HttpStatusCode.OK, data);
//        }
//    }
//}




using BLL.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TieePMS.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("api/category/all")]
        public HttpResponseMessage Get()
        {
            // Check for a valid token in the Authorization header
            var authHeader = Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer" || !UserService.IsTokenValid(authHeader.Parameter))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized access. Please log in.");
            }

            var data = CategoryService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/category/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = CategoryService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/category/{id}/products")]
        public HttpResponseMessage GetwithProducts(int id)
        {
            var data = CategoryService.GetwithProducts(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
