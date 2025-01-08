using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTOs;
using BLL.Services;

namespace PL.Controllers
{
    public class PersonController : ApiController
    {
        [HttpGet]
        [Route("api/persons/all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = PersonService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/persons/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = PersonService.Get(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Person not found");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/persons/create")]
        public HttpResponseMessage Create([FromBody] PersonDTO person)
        {
            try
            {
                if (person == null || string.IsNullOrWhiteSpace(person.Name))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid person data.");
                }

                PersonService.Create(person);
                return Request.CreateResponse(HttpStatusCode.Created, "Person successfully created!!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/persons/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                PersonService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Person successfully deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
