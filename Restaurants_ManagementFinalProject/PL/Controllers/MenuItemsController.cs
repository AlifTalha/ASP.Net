using BLL.DTOs;
using BLL.Services;
using System.IO;
using System.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IntroTier.Controllers
{
    [RoutePrefix("api/menuitems")]
    public class MenuItemsController : ApiController
    {
        //[HttpGet]
        //[Route("")]
        //public HttpResponseMessage GetAll()
        //{
        //    var items = MenuItemService.GetAll();
        //    return Request.CreateResponse(HttpStatusCode.OK, items);
        //}
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["user"] == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please login first.");
            }

            var items = MenuItemService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }


        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetById(int id)
        {
            var item = MenuItemService.GetById(id);
            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create(MenuItemDTO item)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var created = MenuItemService.Create(item);
            if (created)
                return Request.CreateResponse(HttpStatusCode.Created);

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update(MenuItemDTO item)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var updated = MenuItemService.Update(item);
            if (updated)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var deleted = MenuItemService.Delete(id);
            if (deleted)
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
        }


        [HttpGet]
        [Route("category/{category}")]
        public HttpResponseMessage GetByCategory(string category)
        {
            var items = MenuItemService.GetByCategory(category);
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [HttpGet]
        [Route("available")]
        public HttpResponseMessage GetAvailableItems()
        {
            var items = MenuItemService.GetAvailableItems();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search(string name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["user"] == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please login first.");
            }

            var items = MenuItemService.Search(name, minPrice, maxPrice);
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }



        [HttpPost]
        [Route("upload")]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var file = provider.FileData.FirstOrDefault();
                if (file == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No file uploaded");

                var fileName = Path.GetFileName(file.LocalFileName);
                var originalFileName = file.Headers.ContentDisposition.FileName.Trim('"');
                var extension = Path.GetExtension(originalFileName);
                var newFileName = Guid.NewGuid().ToString() + extension;
                var newPath = Path.Combine(root, newFileName);
                File.Move(file.LocalFileName, newPath);

                string relativePath = "/UploadedFiles/" + newFileName;
                return Request.CreateResponse(HttpStatusCode.OK, new { imagePath = relativePath });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


    }
}