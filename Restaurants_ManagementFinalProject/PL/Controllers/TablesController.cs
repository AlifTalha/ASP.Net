using BLL.DTOs;
using BLL.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntroTier.Controllers
{
    [RoutePrefix("api/tables")]
    public class TablesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            var tables = TableService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, tables);
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetById(int id)
        {
            var table = TableService.GetById(id);
            if (table == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [HttpGet]
        [Route("number/{tableNumber}")]
        public HttpResponseMessage GetByTableNumber(int tableNumber)
        {
            var table = TableService.GetByTableNumber(tableNumber);
            if (table == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create(TableDTO table)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var created = TableService.Create(table);
            if (created)
                return Request.CreateResponse(HttpStatusCode.Created);

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update(TableDTO table)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var updated = TableService.Update(table);
            if (updated)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var deleted = TableService.Delete(id);
            if (deleted)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("available")]
        public HttpResponseMessage GetAvailableTables()
        {
            var tables = TableService.GetAvailableTables();
            return Request.CreateResponse(HttpStatusCode.OK, tables);
        }
    }
}